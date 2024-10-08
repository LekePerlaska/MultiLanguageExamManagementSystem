﻿using System.ComponentModel.DataAnnotations.Schema;

namespace MultiLanguageExamManagementSystem.Models.Entities
{
    public class Language
    {
        public int Id {get; set;}
        public string Name {get; set;}
        public int LanguageCode {get; set;}
        public int CountryId {get; set;}
        public Country Country {get; set;}
        public List<LocalizationResource> LocalizationResources;
    }
}
