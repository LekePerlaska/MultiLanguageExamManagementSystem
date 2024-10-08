using Microsoft.AspNetCore.Mvc;
using MultiLanguageExamManagementSystem.Services;

[ApiController]
[Route("/[controller]")]
public class TranslationController : ControllerBase
{
    private readonly ITranslationService _translationService;

    public TranslationController(ITranslationService translationService)
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
