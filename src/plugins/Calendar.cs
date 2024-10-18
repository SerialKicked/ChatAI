using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaifuAI.Files;

namespace WaifuAI.Plugins
{
    public class RecurrencePattern(RecurrenceFrequency frequency, int interval, DateTime? endDate = null)
    {
        public RecurrenceFrequency Frequency { get; set; } = frequency;
        public int Interval { get; set; } = interval;
        public DateTime? EndDate { get; set; } = endDate;
    }

    public enum RecurrenceFrequency
    {
        Daily,
        Weekly,
        Monthly,
        Yearly
    }

    public class CalendarEvent(Guid id, string title, string description, DateTime startTime, DateTime endTime, string location, RecurrencePattern recurrence)
    {
        public Guid Id { get; set; } = id;
        public string Title { get; set; } = title;
        public string Description { get; set; } = description;
        public DateTime StartTime { get; set; } = startTime;
        public DateTime EndTime { get; set; } = endTime;
        public string Location { get; set; } = location;
        public RecurrencePattern Recurrence { get; set; } = recurrence;
    }

    public class Calendar : BaseFile
    {
        public List<CalendarEvent> Events { get; set; } = [];
    }
}
