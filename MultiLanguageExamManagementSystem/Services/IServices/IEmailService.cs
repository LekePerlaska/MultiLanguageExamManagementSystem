
namespace MultiLanguageExamManagementSystem.Services
{
    public interface IEmailService
    {
        Task SendExamResultsAsync(string email, double score);
    }
}
