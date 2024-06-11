using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using MultiLanguageExamManagementSystem.Models.Dtos;
using MultiLanguageExamManagementSystem.Models.Entities;
using MultiLanguageExamManagementSystem.Data;

[ApiController]
[Route("[controller]")]
public class CountriesController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public CountriesController(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public ActionResult<IEnumerable<CountryDto>> GetCountries()
    {
       var countryDtos = _context.Countries
            .ProjectTo<CountryDto>(_mapper.ConfigurationProvider)
            .ToList(); 


        return Ok(countryDtos);
    }

    [HttpGet("GetCountry/{id}")]
    public ActionResult<CountryDto> GetCountry(int id)
    {
        var country = _context.Countries
            .Include(c => c.Languages)
                .ThenInclude(l => l.LocalizationResources)
            .FirstOrDefault(c => c.Id == id);

        if (country == null)
        {
            return NotFound();
        }

        var countryDto = _mapper.Map<CountryDto>(country);

        return Ok(countryDto);
    }

    [HttpPost]
    public ActionResult<CountryDto> CreateCountry(CountryDto countryDto)
    {
        var country = _mapper.Map<Country>(countryDto);
        _context.Countries.Add(country);
        _context.SaveChanges();

        countryDto.Id = country.Id;

        return CreatedAtAction(nameof(GetCountry), new { id = countryDto.Id }, countryDto);
    }

    [HttpPut("UpdateCountry/{id}")]
    public IActionResult UpdateCountry(int id, CountryDto countryDto)
    {
        if (id != countryDto.Id)
        {
            return BadRequest();
        }

        var country = _mapper.Map<Country>(countryDto);
        _context.Entry(country).State = EntityState.Modified;
        _context.SaveChanges();

        return NoContent();
    }

    [HttpDelete("DeleteCountry/{id}")]
    public IActionResult DeleteCountry(int id)
    {
        var country = _context.Countries.Find(id);
        if (country == null)
        {
            return NotFound();
        }

        _context.Countries.Remove(country);
        _context.SaveChanges();

        return NoContent();
    }
}
