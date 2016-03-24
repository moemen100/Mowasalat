using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace wasalney
{
    public class Mowaslat
    {

        public string Id { get; set; }
        [JsonProperty(PropertyName = "Latitude")]
        public Double Latitude { get; set; }
        [JsonProperty(PropertyName = "Longtude")]
        public Double Longtude { get; set; }
        [JsonProperty(PropertyName = "complete")]
        public bool Complete { get; set; }
        [JsonProperty(PropertyName = "Place")]
        public string place { get; set; }
        [JsonProperty(PropertyName = "iden")]
        public int iden { get; set; }
        [JsonProperty(PropertyName = "Type")]
        public string Type { get; set; }
    }
}
