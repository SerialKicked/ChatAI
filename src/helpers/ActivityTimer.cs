using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaifuAI
{
    internal class ActivityTimer
    {
        public event EventHandler? OnTrigger;

        public TimeSpan MinTime = new(0, 15, 0);
        public TimeSpan MaxTime = new(0, 45, 0);
        private DateTime _lastActivity;
        private TimeSpan _setActivityTime = default;
        public int TriggerCount { get; private set; } = 0;

        private readonly Random RNG = new();

        public ActivityTimer()
        {
            Reset();
        }

        public void Reset()
        {
            _lastActivity = DateTime.Now;
            var ticks = RNG.Next((int)MinTime.TotalMinutes, (int)MaxTime.TotalMinutes);
            _setActivityTime = new TimeSpan(0, ticks, 0);
        }

        public bool IsTimeout()
        {
            if ((DateTime.Now - _lastActivity) >= _setActivityTime)
            {
                OnTrigger?.Invoke(this, new EventArgs());
                TriggerCount++;
                Reset();
                return true;
            }
            return false;
        }
    }
}
