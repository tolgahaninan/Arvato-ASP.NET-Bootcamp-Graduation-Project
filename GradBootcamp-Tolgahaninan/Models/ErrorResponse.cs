namespace GradBootcamp_Tolgahaninan.Models
{
    public class ErrorResponse // Model For Error Responses
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }
    }
}
