using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models{
    public class Koncert{
        [Key]
        public int ID { get; set; }

        [Required]
        public string NazivKoncerta {get; set;}
        
        [Required]
        [RegularExpression("^\\d{4}-((0\\d)|(1[012]))-(([012]\\d)|3[01])$", ErrorMessage = "Datum nije ispravnog formata.")]
        [DataType(DataType.Date)]
        public string Datum{get; set;}

        [Required]
        [RegularExpression("^(?:[01]\\d|2[0-3]):[0-5]\\d$", ErrorMessage = "Vreme nije ispravnog formata.")]
        [DataType(DataType.Time)]
        public string Vreme{get; set;}

        [Required]
        [Range(1000, 3000)]
        public int Cena {get; set;}
        
        [Required]
        //[JsonIgnore]
        public Izvodjac Izvodjac { get; set; }
        public Hala Hala { get; set; }
    }
}