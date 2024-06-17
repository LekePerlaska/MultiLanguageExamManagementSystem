using System.Text.Json;

namespace MultiLanguageExamManagementSystem.Services
{
    public interface ITranslationService
	{
    	Task<string> TranslateAsync(string text, string targetLanguage);
	}
}