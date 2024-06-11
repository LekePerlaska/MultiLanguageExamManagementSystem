namespace MultiLanguageExamManagementSystem.Models.Dtos
{
    public class ExamQuestionDto
    {
        public int ExamId { get; set; }
        public ExamDto Exam { get; set; }
        public int QuestionId { get; set; }
        public QuestionDto Question { get; set; }
    }
}
