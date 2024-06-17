using MultiLanguageExamManagementSystem.Models.Dtos;
using MultiLanguageExamManagementSystem.Models.Entities;
using Microsoft.EntityFrameworkCore;
using MultiLanguageExamManagementSystem.Data;

namespace MultiLanguageExamManagementSystem.Services
{
    public class ExamService : IExamService
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmailService _emailService;

        public ExamService(ApplicationDbContext context, IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        public async Task<IEnumerable<ExamDto>> GetAvailableExamsAsync()
        {
            return await _context.Exams
                .Include(e => e.Creator)
                .Select(e => new ExamDto
                {
                    ExamId = e.ExamId,
                    Title = e.Title,
                    CreatorId = e.CreatorId,
                })
                .ToListAsync();
        }

        public async Task RequestExamAsync(RequestExamDto request)
        {
            var takenExam = await _context.TakenExams
                .FirstOrDefaultAsync(te => te.UserId == request.UserId && te.ExamId == request.ExamId);

            if (takenExam != null)
            {
                if (takenExam.AttemptCount >= 3)
                {
                    throw new InvalidOperationException("No more attempts left.");
                }

                takenExam.AttemptCount++;
                takenExam.Status = "Pending";
            }
            else
            {
                takenExam = new TakenExam
                {
                    UserId = request.UserId,
                    ExamId = request.ExamId,
                    AttemptCount = 1,
                    Status = "Pending"
                };
                await _context.TakenExams.AddAsync(takenExam);
            }

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ExamDto>> GetApprovedExamsAsync(int userId)
        {
            return await _context.TakenExams
                .Where(te => te.UserId == userId && te.Status == "Approved")
                .Include(te => te.Exam)
                .ThenInclude(e => e.Creator)
                .Select(te => new ExamDto
                {
                    ExamId = te.Exam.ExamId,
                    Title = te.Exam.Title,
                    CreatorId = te.Exam.CreatorId,
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<QuestionDto>> GetExamQuestionsAsync(int examId)
        {
            return await _context.ExamQuestions
                .Where(eq => eq.ExamId == examId)
                .Include(eq => eq.Question)
                .Select(eq => new QuestionDto
                {
                    QuestionId = eq.QuestionId,
                    Text = eq.Question.Text,
                })
                .ToListAsync();
        }

        public async Task SubmitExamAsync(int userId, ExamSubmissionDto submission)
        {
            var takenExam = await _context.TakenExams
                .FirstOrDefaultAsync(te => te.UserId == userId && te.ExamId == submission.ExamId && te.Status == "Approved");

            if (takenExam == null)
            {
                throw new InvalidOperationException("Exam not approved or does not exist.");
            }

            int correctAnswers = 0;
            foreach (var answer in submission.Answers)
            {
                var question = await _context.Questions.FindAsync(answer.Key);
                if (question != null && question.CorrectAnswer == answer.Value)
                {
                    correctAnswers++;
                }
            }

            int totalQuestions = submission.Answers.Count;
            double score = (double)correctAnswers / totalQuestions * 100;

            takenExam.IsCompleted = true;
            takenExam.Status = "Completed";
            await _context.SaveChangesAsync();

            var user = await _context.Users.FindAsync(userId);
            await _emailService.SendExamResultsAsync(user.Email, score);
        }
    }
}
