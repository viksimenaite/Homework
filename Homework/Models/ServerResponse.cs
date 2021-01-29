using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Homework
{
    public class ServerResponse
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("distance")]
        public int Distance { get; set; }

        public override string ToString()
        {
            return "Server name: " + Name + " Distance: " + Distance;

        }
    }
}
