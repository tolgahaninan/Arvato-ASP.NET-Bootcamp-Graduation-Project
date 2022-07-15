using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GradBootcamp_Tolgahaninan.Models
{
    public class Genre // Model For Genre
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        public string name { get; set; }
    }
}
