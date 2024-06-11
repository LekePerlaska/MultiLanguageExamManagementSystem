namespace MultiLanguageExamManagementSystem.Models.Entities
{
	public class Exam
	{
    	public int ExamId { get; set; }
    	public string Title { get; set; }
    	public int CreatorId { get; set; }
    	public User Creator { get; set; }
   		public ICollection<ExamQuestion> ExamQuestions { get; set; }
	}
}