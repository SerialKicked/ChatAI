using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LetheAISharp;
using LetheAISharp.Files;
using LetheAISharp.LLM;
using LetheChat.Files;

namespace LetheChat.Plugins
{
    public class RecurrencePattern(RecurrenceFrequency frequency, int interval, DateTime? endDate = null)
    {
        public RecurrenceFrequency Frequency { get; set; } = frequency;
        public int Interval { get; set; } = interval;
        public DateTime? EndDate { get; set; } = endDate;
    }

    public enum RecurrenceFrequency
    {
        OneTime,
        Daily,
        Weekly,
        Monthly,
        Yearly
    }

    public class CalendarEvent(Guid id, string title, DateTime startTime, DateTime endTime, string location, RecurrencePattern recurrence)
    {
        public Guid Id { get; set; } = id;
        public string Title { get; set; } = title;
        public DateTime StartTime { get; set; } = startTime;
        public DateTime EndTime { get; set; } = endTime;
        public string Location { get; set; } = location;
        public RecurrencePattern Recurrence { get; set; } = recurrence;
        public bool IsRecurring => Recurrence.Frequency != RecurrenceFrequency.OneTime;
        public bool IsOver = false;

        public bool Update()
        {
            if (!IsRecurring)
            {
                if (DateTime.Now > EndTime)
                {
                    IsOver = true;
                    return true;
                }
                return false;
            }
            switch (Recurrence.Frequency)
            {
                case RecurrenceFrequency.Daily:
                    StartTime = StartTime.AddDays(Recurrence.Interval);
                    EndTime = EndTime.AddDays(Recurrence.Interval);
                    break;
                case RecurrenceFrequency.Weekly:
                    StartTime = StartTime.AddDays(Recurrence.Interval * 7);
                    EndTime = EndTime.AddDays(Recurrence.Interval * 7);
                    break;
                case RecurrenceFrequency.Monthly:
                    StartTime = StartTime.AddMonths(Recurrence.Interval);
                    EndTime = EndTime.AddMonths(Recurrence.Interval);
                    break;
                case RecurrenceFrequency.Yearly:
                    StartTime = StartTime.AddYears(Recurrence.Interval);
                    EndTime = EndTime.AddYears(Recurrence.Interval);
                    break;
            }
            if (Recurrence.EndDate.HasValue && DateTime.Now > Recurrence.EndDate)
            {
                IsOver = true;
                return true;
            }
            return true;
        }
    }

    public class Calendar : BaseFile
    {
        public List<CalendarEvent> Events { get; set; } = [];
        public List<CalendarEvent> PastEvents { get; set; } = [];

        public List<CalendarEvent> GetEvents(DateTime start, DateTime end)
        {
            return Events.FindAll(e => e.StartTime >= start && e.StartTime <= end);
        }

        public List<CalendarEvent> GetEvents(DateTime date)
        {
            return Events.FindAll(e => e.StartTime.Date == date.Date);
        }

        public List<CalendarEvent> GetIncomingEvents(TimeSpan span)
        {
            return Events.FindAll(e => e.StartTime <= DateTime.Now + span);
        }

        public void Update()
        {
            foreach (var e in Events)
            {
                e.Update();
            }
            var lst = Events.FindAll(e => e.IsOver);
            PastEvents.AddRange(lst);
            Events.RemoveAll(e => e.IsOver);
        }

        public void AddEvent(CalendarEvent e)
        {
            Events.Add(e);
        }

        public void RemoveEvent(Guid id)
        {
            Events.RemoveAll(e => e.Id == id);
            PastEvents.RemoveAll(e => e.Id == id);
        }

        public void RemovePastEvents()
        {
            PastEvents.Clear();
        }

        public void RemoveAllEvents()
        {
            Events.Clear();
            PastEvents.Clear();
        }

        public void RemoveEvents(DateTime start, DateTime end)
        {
            Events.RemoveAll(e => e.StartTime >= start && e.StartTime <= end);
        }

        public string InformAINextEvent(TimeSpan span)
        {
            Update();
            var nextevents = GetIncomingEvents(span);
            if (nextevents.Count == 0)
                return string.Empty;

            var str = new StringBuilder("Notify {{user}} of the following events in their calendar:").AppendLinuxLine();
            foreach (var item in nextevents)
            {
                str.AppendLinuxLine($"- On the {StringExtensions.DateToHumanString(item.StartTime)}: {item.Title}");
            }
            return str.ToString();
        }
    }
}
