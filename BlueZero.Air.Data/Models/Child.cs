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
    public class Child
    {
        public const int KeyLength = 16;

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public string Key { get; set; }

        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTime DateCreated { get; set; }

        public string Forename { get; set; }

        public string Surname { get; set; }
        
        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTime DateOfBirth { get; set; }

        public string Notes { get; set; }

        // content://com.android.contacts/data/87
        public string ParentContactUri { get; set; }

        public string RegistrationCode { get; set; }

        public bool Deleted { get; set; }
    }
}