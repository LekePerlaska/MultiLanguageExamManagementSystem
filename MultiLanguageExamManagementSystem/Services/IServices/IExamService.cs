using MultiLanguageExamManagementSystem.Models.Dtos;

namespace MultiLanguageExamManagementSystem.Services
{
    public interface IExamService
    {
        Task<IEnumerable<ExamDto>> GetAvailableExamsAsync();
        Task RequestExamAsync(RequestExamDto request);
        Task<IEnumerable<ExamDto>> GetApprovedExamsAsync(int userId);
        Task<IEnumerable<QuestionDto>> GetExamQuestionsAsync(int examId);
        Task SubmitExamAsync(int userId, ExamSubmissionDto submission);
    }
}
