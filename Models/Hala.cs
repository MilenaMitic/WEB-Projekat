using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models{

    [Table("Hala")]
    public class Hala
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [MaxLength(50)]
        public string Naziv { get; set; }

        [JsonIgnore]
        public List<Koncert> Koncerti { get; set; }

        public List<Karta> Karte { get; set; }
        
    }
}