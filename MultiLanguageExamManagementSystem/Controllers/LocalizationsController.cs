using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using MultiLanguageExamManagementSystem.Services.IServices;
using MultiLanguageExamManagementSystem.Services;
using MultiLanguageExamManagementSystem.Models.Dtos;

namespace MultiLanguageExamManagementSystem.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class LocalizationResourceController : ControllerBase
	{
    	private readonly ICultureService _cultureService;
    	private readonly TranslationService _translationService;
    	private readonly ILogger<LocalizationResourceController> _logger;

    	public LocalizationResourceController(ICultureService cultureService, TranslationService translationService, ILogger<LocalizationResourceController> logger)
    	{
        	_cultureService = cultureService;
        	_translationService = translationService;
        	_logger = logger;
    	}

    	[HttpPost("AddLocalizationResources")]
    	public IActionResult AddLocalizationResource(LocalizationResourceDto resource)
    	{
        	try
        	{
            	_cultureService.AddLocalizationResource(resource);
            	_logger.LogInformation("Localization resource added successfully.");

            	var languages = _cultureService.GetAllLanguages();
            	var otherLanguages = languages.Where(l => l.Id != resource.LanguageId).ToList();

            	foreach (var language in otherLanguages)
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
            	_logger.LogError(ex, "Error adding localization resource.");
            	return StatusCode(500, "Internal server error");
        	}
    	}
    	[HttpGet("GetLocalizationResources")]
    	public ActionResult<List<LocalizationResourceDto>> GetLocalizationResources()
    	{
        	try
        	{
            	var resources = _cultureService.GetAllLocalizationResources();
            	return Ok(resources);
        	}
        	catch (Exception ex)
        	{
            	_logger.LogError(ex, "Error retrieving localization resources.");
            	return StatusCode(500, "Internal server error");
        	}
    	}

    	[HttpGet("GetLocalizationResources/{id}")]
    	public ActionResult<LocalizationResourceDto> GetLocalizationResource(int id)
    	{
        	try
        	{
            	var resource = _cultureService.GetLocalizationResource(id);
            	if (resource == null)
            	{
                	_logger.LogWarning($"Localization resource with ID {id} not found.");
                	return NotFound();
            	}
            	return Ok(resource);
        	}
        	catch (Exception ex)
        	{
            	_logger.LogError(ex, $"Error retrieving localization resource with ID {id}.");
            	return StatusCode(500, "Internal server error");
        	}
    	}


    	[HttpPut("UpdateLocalizationResources/{id}")]
    	public IActionResult UpdateLocalizationResource(int id, LocalizationResourceDto resource)
    	{
        	try
        	{
            	var existingResource = (LocalizationResourceDto)_cultureService.GetLocalizationResource(id);
            	if (existingResource == null)
            	{
                	_logger.LogWarning($"Localization resource with ID {id} not found.");
                	return NotFound();
            	}

            	existingResource.LanguageId = resource.LanguageId;
            	existingResource.Namespace = resource.Namespace;
            	existingResource.Key = resource.Key;
            	existingResource.Value = resource.Value;

            	_cultureService.UpdateLocalizationResourceAsync(existingResource);
            	_logger.LogInformation($"Localization resource with ID {id} updated successfully.");

            	return NoContent();
        	}
        	catch (Exception ex)
        	{
            	_logger.LogError(ex, $"Error updating localization resource with ID {id}.");
            	return StatusCode(500, "Internal server error");
        	}
    	}

    	[HttpDelete("DeleteLocalizationResources/{id}")]
    	public async Task<IActionResult> DeleteLocalizationResource(int id)
    	{
        	try
        	{
            	var existingResource = _cultureService.GetLocalizationResource(id);
            	if (existingResource == null)
            	{
                	_logger.LogWarning($"Localization resource with ID {id} not found.");
                	return NotFound();
            	}

            	await _cultureService.DeleteLocalizationResourceAsync(id);
            	_logger.LogInformation($"Localization resource with ID {id} deleted successfully.");
            	return NoContent();
        	}
        	catch (Exception ex)
        	{
            	_logger.LogError(ex, $"Error deleting localization resource with ID {id}.");
            	return StatusCode(500, "Internal server error");
        	}
    	}
	}
    
}
