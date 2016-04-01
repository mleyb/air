using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlueZero.Air.Data.Models
{
    public class Sleep
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTime Date { get; set; }

        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTime Start { get; set; }

        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTime End { get; set; }

        // Navigation properties
        [JsonIgnore]
        public Child Child { get; set; }
    }
}