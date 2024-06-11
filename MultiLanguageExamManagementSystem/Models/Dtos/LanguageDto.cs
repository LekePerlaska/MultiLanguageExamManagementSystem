namespace MultiLanguageExamManagementSystem.Models.Dtos
{
    public class LanguageDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int LanguageCode { get; set; }
    public int CountryId { get; set; }
    public List<LocalizationResourceDto> LocalizationResources { get; set; }
}

}