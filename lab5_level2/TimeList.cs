
using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;


namespace lab5_level2
{
    public class TimesList
    {
        public override string ToString()
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            string result = JsonSerializer.Serialize<List<TimeItem>>(TimeItems, options);
            return result;
        }
        private void CopyProperties(List<TimeItem> timesList)
        {
            this.TimeItems = timesList;
        }
        [DataMember]
        private List<TimeItem> TimeItems = new List<TimeItem>();
        public void Add(TimeItem timeItem)
        {
            TimeItems.Add(timeItem);
        }
        public bool Save(string filename)
        {
            try
            {
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                string jsonString = JsonSerializer.Serialize<List<TimeItem>>(TimeItems, options);
                File.WriteAllText(filename, jsonString);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }
        }
        public bool Load(string filename)
        {
            try
            {
                string jsonString = File.ReadAllText(filename);
                List<TimeItem> ti = JsonSerializer.Deserialize<List<TimeItem>>(jsonString);
                this.CopyProperties(ti);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }

        }
    }
}
