using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models{
    public class Karta{
        [Key]
        public int ID { get; set; }

        [Required]
        [Range(1, 20)]
        public int BrojSedista {get; set;}
        
        //[JsonIgnore]
        [ForeignKey("RezervacijaID")]
        public Rezervacija Rezervacija {get; set;}

        //[JsonIgnore]
        public Koncert Koncert {get; set;}

        public Hala Hala { get; set; }

    }
}