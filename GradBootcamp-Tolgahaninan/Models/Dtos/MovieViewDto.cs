namespace GradBootcamp_Tolgahaninan.Models.Dtos
{
    public class MovieViewDto
    {
        public int Id { get; set; }
        public virtual Movie movie { get; set; }
        public int ClickCounter { get; set; }
    }
}
