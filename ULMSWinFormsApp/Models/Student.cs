namespace ULMSWinFormsApp.Models
{
    public class Student
    {
        public string StudentId { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Programme { get; set; } = string.Empty;
    }
}