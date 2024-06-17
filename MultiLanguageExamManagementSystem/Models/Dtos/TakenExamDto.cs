namespace MultiLanguageExamManagementSystem.Models.Dtos
{
    public class TakenExamDto
    {
        public int TakenExamId { get; set; }
        public int UserId { get; set; }
        public int ExamId { get; set; }
        public bool IsCompleted { get; set; }
		public string Status { get; set; }
		public int AttemptCount { get; set; }
    }
}
