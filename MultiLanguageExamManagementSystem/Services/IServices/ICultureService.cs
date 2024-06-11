using LifeEcommerce.Helpers;
using Microsoft.Extensions.Localization;
using MultiLanguageExamManagementSystem.Models.Dtos;

namespace MultiLanguageExamManagementSystem.Services.IServices
{
    public interface ICultureService
    {
        // Your code here
        string GetLocalizedString(int languageId, string namespaceName, string key);
		void AddLanguage(LanguageDto resource);

        LanguageDto GetLanguageById(int languageId);

		IQueryable<LanguageDto> GetAllLanguages();

        Task UpdateLanguageAsync(LanguageDto resource);

        Task DeleteLanguageAsync(int resourceId);

        void AddLocalizationResource(LocalizationResourceDto resource);

		IQueryable<LocalizationResourceDto> GetLocalizationResource(int resourceId);

    	IQueryable<LocalizationResourceDto> GetLocalizationResourcesByLanguage(int languageId);

    	IQueryable<LocalizationResourceDto> GetAllLocalizationResources();

		Task UpdateLocalizationResourceAsync(LocalizationResourceDto resource);

    	Task DeleteLocalizationResourceAsync(int resourceId);

    }
}
