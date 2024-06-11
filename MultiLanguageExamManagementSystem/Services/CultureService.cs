using LifeEcommerce.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using MultiLanguageExamManagementSystem.Data.UnitOfWork;
using MultiLanguageExamManagementSystem.Models.Dtos;
using MultiLanguageExamManagementSystem.Services.IServices;
using System.Globalization;

namespace MultiLanguageExamManagementSystem.Services
{
    public class CultureService : ICultureService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CultureService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // Your code here

        #region String Localization

        public string GetLocalizedString(int languageId, string namespaceName, string key)
        {
            var localizationResourceRepository = _unitOfWork.Repository<LocalizationResourceDto>();
            var resource = localizationResourceRepository.GetByCondition(r => r.LanguageId == languageId && r.Namespace == namespaceName && r.Key == key);
            return resource.ToString();
        }

        #endregion

        #region Languages

		public void AddLanguage(LanguageDto resource)
		{
        	var repository = _unitOfWork.Repository<LanguageDto>();
        	repository.Create(resource);
        	repository.SaveChangesAsync();
		}

        public LanguageDto GetLanguageById(int languageId)
        {
        	return (LanguageDto)_unitOfWork.Repository<LanguageDto>().GetById(r => r.Id ==languageId);
        }

		public IQueryable<LanguageDto> GetAllLanguages()
    	{
     		return _unitOfWork.Repository<LanguageDto>().GetAll();
    	}

		public async Task UpdateLanguageAsync(LanguageDto resource)
    	{
     		var repository = _unitOfWork.Repository<LanguageDto>();
        	repository.Update(resource);
        	repository.SaveChangesAsync();
    	}

    	public async Task DeleteLanguageAsync(int resourceId)
    	{
        	var repository = _unitOfWork.Repository<LanguageDto>();
        	var resource = (LanguageDto)repository.GetById(r => r.Id == resourceId);
        	if (resource != null)
        	{
        	    repository.Delete(resource);
        	    repository.SaveChangesAsync();
        	}
    	}

        #endregion

        #region Localization Resources

        public void AddLocalizationResource(LocalizationResourceDto resource)
    	{
        	var repository = _unitOfWork.Repository<LocalizationResourceDto>();
        	repository.Create(resource);
        	repository.SaveChangesAsync();
    	} 

		public IQueryable<LocalizationResourceDto> GetLocalizationResource(int resourceId)
    	{
        	var repository = _unitOfWork.Repository<LocalizationResourceDto>();
        	return repository.GetById(r => r.Id == resourceId);
    	}

    	public IQueryable<LocalizationResourceDto> GetLocalizationResourcesByLanguage(int languageId)
    	{
     		var repository = _unitOfWork.Repository<LocalizationResourceDto>();
        	return repository.GetByCondition(r => r.LanguageId == languageId);
    	}

    	public IQueryable<LocalizationResourceDto> GetAllLocalizationResources()
    	{
        	var repository = _unitOfWork.Repository<LocalizationResourceDto>();
        	return repository.GetAll();
    	}

		public async Task UpdateLocalizationResourceAsync(LocalizationResourceDto resource)
    	{
     		var repository = _unitOfWork.Repository<LocalizationResourceDto>();
        	repository.Update(resource);
        	repository.SaveChangesAsync();
    	}

    	public async Task DeleteLocalizationResourceAsync(int resourceId)
    	{
        	var repository = _unitOfWork.Repository<LocalizationResourceDto>();
        	var resource = (LocalizationResourceDto)repository.GetById(r => r.Id == resourceId);
        	if (resource != null)
        	{
        	    repository.Delete(resource);
        	    repository.SaveChangesAsync();
        	}
    	}

        #endregion
    }
}
