using System.Text.Json;

namespace MultiLanguageExamManagementSystem.Services
{
    public class TranslationService
	{
 		private readonly HttpClient _httpClient;
    	private readonly string _apiKey;

    	public TranslationService(HttpClient httpClient, string apiKey)
    	{
        	_httpClient = httpClient;
        	_apiKey = apiKey;
    	}

    	public async Task<string> TranslateAsync(string text, string targetLanguage)
    	{
        	var requestContent = new FormUrlEncodedContent(new[]
        	{
            	new KeyValuePair<string, string>("auth_key", _apiKey),
            	new KeyValuePair<string, string>("text", text),
            	new KeyValuePair<string, string>("target_lang", targetLanguage)
        	});

        	var response = await _httpClient.PostAsync("https://api.deepl.com/v2/translate", requestContent);
        
        	if (!response.IsSuccessStatusCode)
        	{
         		throw new Exception($"Failed to translate text. Status code: {response.StatusCode}");
        	}

        	var responseContent = await response.Content.ReadAsStringAsync();
        	var translationResponse = JsonSerializer.Deserialize<TranslationResponse>(responseContent);

        	return translationResponse?.Translations?[0]?.Text;
    	}
	}
}
public class TranslationResponse
{
    public Translation[] Translations { get; set; }
}

public class Translation
{
    public string Text { get; set; }
}

