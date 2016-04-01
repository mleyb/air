using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlueZero.Air.Data.Models
{    
    public class Carer
    {
        public const int KeyLength = 16;

        public string Id { get; set; }

        public string Key { get; set; }

        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTime DateCreated { get; set; }

        public string Name { get; set; }

        [JsonIgnore]
        public string Email { get; set; }

        // Navigation properties
        public ICollection<Child> Children { get; set; }
    }
}