namespace MultiLanguageExamManagementSystem.Models.Entities
{
    public class TakenExam
    {
        public int TakenExamId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int ExamId { get; set; }
        public Exam Exam { get; set; }
        public bool IsCompleted { get; set; }
		public string Status { get; set; }
		public int AttemptCount { get; set; }
    }
}
