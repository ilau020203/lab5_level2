using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace lab5_level2
{
    public class TimeItem
    {
        [DataMember]
        public int Order { get; set; }
        [DataMember]
        public int RepeatNumber { get; set; }
        [DataMember]
        public int CSTime { get; set; }
        [DataMember]
        public int CPPTime { get; set; }
        [DataMember]
        public double TimeCoefficient { get; set; }
        public TimeItem(int order, int repeatNumber, int cstime, int cpptime)
        {
            Order = order;
            RepeatNumber = repeatNumber;
            CSTime = cstime;
            CPPTime = cpptime;
            TimeCoefficient = Math.Round((double)CSTime / CPPTime, 3);
        }
        public TimeItem() { }

    }
}
