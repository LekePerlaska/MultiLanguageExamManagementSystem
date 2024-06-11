using Microsoft.AspNetCore.Mvc;
using MultiLanguageExamManagementSystem.Models.Dtos;
using MultiLanguageExamManagementSystem.Services;
using MultiLanguageExamManagementSystem.Services.IServices;

namespace MultiLanguageExamManagementSystem.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class LanguageController : ControllerBase
	{
		private readonly ICultureService _cultureService;
		private readonly TranslationService _translationService;
		private readonly ILogger<LanguageController> _logger;

		public LanguageController(ICultureService cultureService, TranslationService translationService, ILogger<LanguageController> logger)
		{
			_cultureService = cultureService;
			_translationService = translationService;
			_logger = logger;
		}

		[HttpGet]
		public ActionResult<List<LanguageDto>> GetLanguages()
		{
			try
			{
				var languages = _cultureService.GetAllLanguages();
				return Ok(languages);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error retrieving languages.");
				return StatusCode(500, "Internal server error");
			}
		}

		[HttpGet("{id}")]
		public ActionResult<LanguageDto> GetLanguage(int id)
		{
			try
			{
				var language = _cultureService.GetLanguageById(id);
				if (language == null)
				{
					_logger.LogWarning($"Language with ID {id} not found.");
					return NotFound();
				}
				return Ok(language);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, $"Error retrieving language with ID {id}.");
				return StatusCode(500, "Internal server error");
			}
		}

		[HttpPost]
		public IActionResult AddLanguage(LanguageDto language)
		{
			try
			{
				_cultureService.AddLanguage(language);
				_logger.LogInformation("Language added successfully.");

				var englishResources = _cultureService.GetLocalizationResourcesByLanguage(1);

				foreach (var resource in englishResources)
				{
					var translatedText = _translationService.TranslateAsync(resource.Value, language.Name);
					var newResource = new LocalizationResourceDto
					{
						LanguageId = language.Id,
						Namespace = resource.Namespace,
						Key = resource.Key,
						Value = translatedText.ToString()
					};
					_cultureService.AddLocalizationResource(newResource);
				}

				return Ok();
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error adding language.");
				return StatusCode(500, "Internal server error");
			}
		}

		[HttpPut("{id}")]
		public IActionResult UpdateLanguage(int id, LanguageDto language)
		{
			try
			{
				var existingLanguage = _cultureService.GetLanguageById(id);
				if (existingLanguage == null)
				{
					_logger.LogWarning($"Language with ID {id} not found.");
					return NotFound();
				}

				existingLanguage.Name = language.Name;

				_cultureService.UpdateLanguageAsync(existingLanguage);
				_logger.LogInformation($"Language with ID {id} updated successfully.");

				return NoContent();
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, $"Error updating language with ID {id}.");
				return StatusCode(500, "Internal server error");
			}
		}

		[HttpDelete("{id}")]
		public IActionResult DeleteLanguage(int id)
		{
			try
			{
				var existingLanguage = _cultureService.GetLanguageById(id);
				if (existingLanguage == null)
				{
					_logger.LogWarning($"Language with ID {id} not found.");
					return NotFound();
				}

				_cultureService.DeleteLanguageAsync(id);
				_logger.LogInformation($"Language with ID {id} deleted successfully.");
				return NoContent();
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, $"Error deleting language with ID {id}.");
				return StatusCode(500, "Internal server error");
			}
		}
	}
}