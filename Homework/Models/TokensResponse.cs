using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Homework
{
    public class TokensResponse
    {
        [JsonProperty("token")]
        public string Token { get; set; }
    }
}
