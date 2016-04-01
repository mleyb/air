﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlueZero.Air.Data.Models
{
    public class Activity
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTime Date { get; set; }

        public string Detail { get; set; }

        public string Description { get; set; }

        // Navigation properties
        [JsonIgnore]
        public Child Child { get; set; }
    }
}