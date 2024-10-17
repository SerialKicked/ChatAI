using HNSW.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Numerics;
using Newtonsoft.Json.Linq;
using MessagePack;
using Microsoft.Extensions.Logging;
using WaifuAI.Files;

namespace WaifuAI.Memory
{
    /// <summary>
    /// Basic RNG for the SmallWorld implementation (not thread safe)
    /// </summary>
    class RNGPlus : IProvideRandomValues
    {
        private readonly Random RNG = new();
        public bool IsThreadSafe => false;
        public float NextFloat() => (float)RNG.NextDouble();
        public int Next(int minValue, int maxValue) => RNG.Next(minValue, maxValue);
        public void NextFloats(Span<float> buffer)
        {
            for (int i = 0; i < buffer.Length; i++)
                buffer[i] = (float)RNG.NextDouble();
        }
    }

    /// <summary>
    /// Thread-safe RNG for the SmallWorld implementation
    /// </summary>
    class ThreadSafeRNG : IProvideRandomValues
    {
        private readonly ThreadLocal<Random> threadLocalRandom = new(() => new Random(Interlocked.Increment(ref seed)));
        private static int seed = Environment.TickCount;

        //private readonly Random RNG = new();
        public bool IsThreadSafe => true;
        public float NextFloat() => (float)threadLocalRandom.Value!.NextDouble();
        public int Next(int minValue, int maxValue) => threadLocalRandom.Value!.Next(minValue, maxValue);
        public void NextFloats(Span<float> buffer)
        {
            var rng = threadLocalRandom.Value;
            for (int i = 0; i < buffer.Length; i++)
                buffer[i] = (float)rng!.NextDouble();
        }
    }

    record VectorSearchResult 
    {
        public Guid ID;
        public EmbedType Category;
        public float Distance;
        public VectorSearchResult(Guid id, EmbedType category, float dist)
        {
            ID = id;
            Category = category;
            Distance = dist;
        }
    }

    public enum EmbedType { Title, Summary, Document }

    class SmallWorldDB
    {
        public bool UseSummaries { get; set; } = true;
        public bool UseTitles { get; set; } = true;

        public bool IsLoaded { get; private set; } = false;
        public SmallWorld<float[], float> World { get; private set; } = null!;
        public int Count => World?.Items?.Count ?? 0;

        public Dictionary<int, (Guid ID, EmbedType embedType)> LookupDB { get; private set; } = [];

        private readonly int M;
        private readonly NeighbourSelectionHeuristic Heuristic;

        public SmallWorldDB(int valueM = 15, NeighbourSelectionHeuristic heuristic = NeighbourSelectionHeuristic.SelectHeuristic)
        {
            M = valueM;
            Heuristic = heuristic;
            Reset();
        }

        public void Reset()
        {
            var parameters = new SmallWorld<float[], float>.Parameters()
            {
                M = this.M,
                LevelLambda = 1 / Math.Log(this.M),
                NeighbourHeuristic = this.Heuristic,
            };
            World = new SmallWorld<float[], float>(Vector.IsHardwareAccelerated ? CosineDistance.SIMDForUnits : CosineDistance.ForUnits, new ThreadSafeRNG(), parameters, true);
            IsLoaded = false;
        }

        public void ImportChatlog(Chatlog log)
        {
            if (IsLoaded)
                Reset();
            if (log.Sessions.Count == 0)
                return;

            var vectors = new List<float[]>();
            LookupDB = new Dictionary<int, (Guid ID, EmbedType embedType)>();
            var currentID = 0;

            for (int i = 0; i < log.Sessions.Count; i++)
            {
                var session = log.Sessions[i];
                if (session.EmbedTitle.Length == 0 || session.EmbedSummary.Length == 0)
                    continue;
                if (UseTitles)
                {
                    vectors.Add(session.EmbedTitle);
                    LookupDB[currentID] = (session.Guid, EmbedType.Title);
                    currentID++;
                }
                if (UseSummaries)
                {
                    vectors.Add(session.EmbedSummary);
                    LookupDB[currentID] = (session.Guid, EmbedType.Summary);
                    currentID++;
                }
            }
            try
            {
                World.AddItems(vectors);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            IsLoaded = true;
        }

        public List<VectorSearchResult> Search(float[] message, int count)
        {
            LLMSystem.logger?.LogInformation("LTM Size: {size} out of {logsize}", Count.ToString(), LLMSystem.History.Sessions.Count.ToString());
            if (!IsLoaded || Count == 0)
                return [];
            var found = World.KNNSearch(message, count);
            var res = new List<VectorSearchResult>();
            foreach (var item in found)
            {
                res.Add(new VectorSearchResult(LookupDB[item.Id].ID, LookupDB[item.Id].embedType, item.Distance));
                LLMSystem.logger?.LogInformation("LTM Found: {id} ({distance})", item.Id.ToString(), item.Distance.ToString());
            }
            res.Sort((a, b) => a.Distance.CompareTo(b.Distance));
            return res;
        }

        public void SaveListToFile(string filePath)
        {
            var tosave = World.Items;
            byte[] bytes = MessagePackSerializer.Serialize(tosave);
            File.WriteAllBytes(filePath, bytes);
        }

        public void LoadListFromFile(string filePath)
        {
            Reset();
            byte[] bytes = File.ReadAllBytes(filePath);
            var x = MessagePackSerializer.Deserialize<IReadOnlyList<float[]>>(bytes);
            if (x == null || x.Count == 0)
                return;
            World.AddItems(x);
            IsLoaded = true;
        }
    }
}
