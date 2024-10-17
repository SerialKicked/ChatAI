using LLama;
using LLama.Common;
using LLama.Extensions;
using WaifuAI.Files;

namespace WaifuAI.Memory
{
    static class RAGSystem
    {
        public static int EmbeddingSize { get; private set; } = 384;

        public static bool Enabled
        {
            get => enabled;
            set
            {
                enabled = value;
                if (!enabled)
                    UnloadEmbedder();
            }
        }
        // Embedding model's weights and params
        private static ModelParams? EmbedSettings = null;
        private static LLamaWeights? EmbedWeights = null;
        private static LLamaEmbedder? Embedder = null;
        private static bool enabled = true;

        public static SmallWorldDB VectorDB { get; private set; } = new();

        public static void Init()
        {
        }

        /// <summary>
        /// Load the Embedding model in memory
        /// </summary>
        /// <returns></returns>
        private static LLamaEmbedder LoadEmbedder()
        {
            if (EmbedSettings != null)
                UnloadEmbedder();
            EmbedSettings = new ModelParams(string.Format("data/models/{0}.gguf", "gte-large.Q6_K"))
            { 
                GpuLayerCount = 255,
                Embeddings = true
            };
            EmbeddingSize = 1024;
            EmbedWeights = LLamaWeights.LoadFromFile(EmbedSettings);
            Embedder = new LLamaEmbedder(EmbedWeights, EmbedSettings);
            
            return Embedder;
        }

        /// <summary>
        /// Unload the Embedding model from memory (if any model loaded)
        /// </summary>
        private static void UnloadEmbedder()
        {
            if (Embedder != null)
            {
                EmbedWeights?.Dispose();
                Embedder?.Dispose();
                Embedder = null;
                EmbedWeights = null;
                EmbedSettings = null;
            }
        }

        /// <summary>
        /// Embedding of all the messages in the chatlog
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        public static async Task EmbedChatSessions(Chatlog log)
        {
            if (!Enabled)
                return;
            var embed = Embedder ?? LoadEmbedder();
            // Embed all the messages in the chatlog except the 80 last ones
            foreach (var session in log.Sessions)
            {
                await session.GenerateEmbeds();
            }
        }

        /// <summary>
        /// Embdding of a single message (async)
        /// </summary>
        /// <param name="textToEmbed"></param>
        /// <returns></returns>
        public static async Task<float[]> EmbeddingText(string textToEmbed)
        {
            if (!Enabled)
                return [];
            var embed = Embedder ?? LoadEmbedder();
            var emb = textToEmbed;
            if (emb.Length > RAGSystem.EmbeddingSize)
                emb = emb[..RAGSystem.EmbeddingSize];
            var tsk = await embed.GetEmbeddings(emb);
            return tsk[0].EuclideanNormalization();
        }

        public static void VectorizeChatlog(Chatlog log)
        {
            if (!Enabled)
                return;
            VectorDB.ImportChatlog(log);
        }

        public static async Task<List<Files.ChatSession>> Search(string message, int count)
        {
            if (!Enabled)
                return [];
            var emb = await EmbeddingText(message);
            var res = VectorDB.Search(emb, count);
            var list = new List<Files.ChatSession>();
            foreach (var item in res)
            {
                if (item == null)
                    continue;
                var found = LLMSystem.History.GetSessionByID(item.ID);
                if (found != null)
                    list.Add(found);
            }
            return list;
        }
    }
}
