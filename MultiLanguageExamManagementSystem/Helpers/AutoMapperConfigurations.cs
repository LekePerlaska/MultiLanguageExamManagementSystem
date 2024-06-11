using AutoMapper;
using MultiLanguageExamManagementSystem.Models.Entities;
using MultiLanguageExamManagementSystem.Models.Dtos;

namespace LifeEcommerce.Helpers
{
    public class AutoMapperConfigurations : Profile
    {
        public AutoMapperConfigurations() 
        {
            // Your code here
            CreateMap<Country, CountryDto>()
                .ForMember(dest => dest.Languages, opt => opt.MapFrom(src => src.Languages));

            CreateMap<Language, LanguageDto>()
                .ForMember(dest => dest.LocalizationResources, opt => opt.MapFrom(src => src.LocalizationResources));

            CreateMap<LocalizationResource, LocalizationResourceDto>();
        
        	CreateMap<CountryDto, Country>()
            	.ForMember(dest => dest.Languages, opt => opt.MapFrom(src => src.Languages));

        	CreateMap<LanguageDto, Language>()
            	.ForMember(dest => dest.LocalizationResources, opt => opt.MapFrom(src => src.LocalizationResources));

        	CreateMap<LocalizationResourceDto, LocalizationResource>(); 
        }
    }
}
