using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProjektUl.Classes
{
    internal class LogEntry
    {
        public DateTime SimulationTime { get; set; }
        public int Day { get; set; }
        public string EventType { get; set; }
        public string Description { get; set; }
        public Dictionary<string, object> Data { get; set; }

        public LogEntry(DateTime simulationTime, int day, string eventType, string description)
        {
            SimulationTime = simulationTime;
            Day = day;
            EventType = eventType;
            Description = description;
        }
    }
}
