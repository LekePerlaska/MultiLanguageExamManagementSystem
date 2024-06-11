namespace MultiLanguageExamManagementSystem.Models.Entities
{
    public class Question
    {
        public int QuestionId { get; set; }
        public string Text { get; set; }
        public ICollection<ExamQuestion> ExamQuestions { get; set; }
    }
}
