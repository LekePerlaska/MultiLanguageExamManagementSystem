namespace MultiLanguageExamManagementSystem.Models.Dtos
{
    public class ExamDto
    {
        public int ExamId { get; set; }
        public string Title { get; set; }
        public int CreatorId { get; set; }
        public ICollection<ExamQuestionDto> ExamQuestions { get; set; }
    }
}
