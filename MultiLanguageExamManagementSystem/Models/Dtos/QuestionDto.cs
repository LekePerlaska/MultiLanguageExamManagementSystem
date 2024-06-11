namespace MultiLanguageExamManagementSystem.Models.Dtos
{
    public class QuestionDto
    {
        public int QuestionId { get; set; }
        public string Text { get; set; }
        public ICollection<ExamQuestionDto> ExamQuestions { get; set; }
    }
}
