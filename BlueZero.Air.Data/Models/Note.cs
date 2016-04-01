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
    public class Note
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTime Date { get; set; }

        public string Detail { get; set; }

        // Navigation properties
        [JsonIgnore]
        public Child Child { get; set; }
    }
}