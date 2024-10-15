using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaifuAI.Files;

namespace WaifuAI.Plugins
{
    public class RecurrencePattern
    {
        public RecurrenceFrequency Frequency { get; set; }
        public int Interval { get; set; } // e.g., every 2 days, every 3 weeks
        public DateTime? EndDate { get; set; } // null if it never ends

        public RecurrencePattern(RecurrenceFrequency frequency, int interval, DateTime? endDate = null)
        {
            Frequency = frequency;
            Interval = interval;
            EndDate = endDate;
        }
    }

    public enum RecurrenceFrequency
    {
        Daily,
        Weekly,
        Monthly,
        Yearly
    }

    public class CalendarEvent
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Location { get; set; }
        public RecurrencePattern Recurrence { get; set; }

        public CalendarEvent()
        {
            Id = Guid.NewGuid();
        }
    }

    public class Calendar : BaseFile
    {
        public List<CalendarEvent> Events { get; set; } = new();
    }
}
