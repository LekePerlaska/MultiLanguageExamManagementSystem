using Microsoft.AspNetCore.Mvc;
using MultiLanguageExamManagementSystem.Models.Dtos;
using MultiLanguageExamManagementSystem.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MultiLanguageExamManagementSystem.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExamController : ControllerBase
    {
        private readonly IExamService _examService;

        public ExamController(IExamService examService)
        {
            _examService = examService;
        }

        [HttpPost("request")]
        public async Task<ActionResult> RequestExam([FromBody] RequestExamDto request)
        {
            try
            {
                await _examService.RequestExamAsync(request);
                return Ok("Request submitted.");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("submit")]
        public async Task<ActionResult> SubmitExam([FromBody] ExamSubmissionDto submission)
        {
            try
            {
                await _examService.SubmitExamAsync(submission.UserId, submission);
                return Ok("Exam submitted and results sent.");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("available")]
        public async Task<ActionResult<IEnumerable<ExamDto>>> GetAvailableExams()
        {
            var exams = await _examService.GetAvailableExamsAsync();
            return Ok(exams);
        }

        [HttpGet("approved/{userId}")]
        public async Task<ActionResult<IEnumerable<ExamDto>>> GetApprovedExams(int userId)
        {
            var exams = await _examService.GetApprovedExamsAsync(userId);
            return Ok(exams);
        }

        [HttpGet("{examId}/questions")]
        public async Task<ActionResult<IEnumerable<QuestionDto>>> GetExamQuestions(int examId)
        {
            var questions = await _examService.GetExamQuestionsAsync(examId);
            return Ok(questions);
        }

    }
}
