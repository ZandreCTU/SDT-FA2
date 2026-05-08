namespace ULMSWinFormsApp.Models
{
    public class MarkRecord
    {
        public string StudentId { get; set; } = string.Empty;
        public string StudentName { get; set; } = string.Empty;
        public double Subject1 { get; set; }
        public double Subject2 { get; set; }
        public double Subject3 { get; set; }
        public double Average { get; set; }
        public string ResultStatus { get; set; } = string.Empty;
    }
}