
namespace MultiLanguageExamManagementSystem.Models.Dtos
{
    public class ExamSubmissionDto
    {
        public int ExamId { get; set; }
		public int UserId { get; set; }
        public Dictionary<int, string> Answers { get; set; } 
    }
}
