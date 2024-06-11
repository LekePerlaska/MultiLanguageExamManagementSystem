using Microsoft.AspNetCore.Mvc;
using MultiLanguageExamManagementSystem.Services;

[ApiController]
[Route("api/[controller]")]
public class TranslationController : ControllerBase
{
    private readonly TranslationService _translationService;

    public TranslationController(TranslationService translationService)
    {
        _translationService = translationService;
    }

    [HttpGet]
    public async Task<ActionResult<string>> TranslateText(string text, string targetLanguage)
    {
        try
        {
            var translatedText = await _translationService.TranslateAsync(text, targetLanguage);
            return Ok(translatedText);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while translating text: {ex.Message}");
        }
    }
}
