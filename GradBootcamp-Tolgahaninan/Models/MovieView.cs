using System.ComponentModel.DataAnnotations;

namespace GradBootcamp_Tolgahaninan.Models
{
    public class MovieView // Model For MovieView
    {
        [Key]  
        public int Id { get; set; }

        [Required]
        public virtual Movie movie{ get; set; }

        [Required]
        public int ClickCounter { get; set; }


    }
}
