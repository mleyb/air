using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlueZero.Air.Data.Models
{
    public class FirstAid
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTime Date { get; set; }

        public string Description { get; set; }

        public string Reason { get; set; }

        public string Detail { get; set; }

        public bool DoctorRecommended { get; set; }

        // Navigation properties
        [JsonIgnore]
        public Child Child { get; set; }
    }
}
