using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models{
    public class Rezervacija{
        [Key]
        public int ID { get; set; }

        [Required]
        [Range(100000, 999999)]
        public int SBR { get; set; }
        
        [Required]
        [MaxLength(25)]
        public string Ime{get; set;}

        [Required]
        [MaxLength(25)]
        public string Prezime{get; set;}

        [Required]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail nije validan!")]
        public string email{get; set;}
        
        [JsonIgnore]
        public Karta Karta {get; set;}
    }
}