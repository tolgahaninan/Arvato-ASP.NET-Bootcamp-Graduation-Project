using AutoMapper;
using GradBootcamp_Tolgahaninan.Models;
using GradBootcamp_Tolgahaninan.Models.Dtos;
using GradBootcamp_Tolgahaninan.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GradBootcamp_Tolgahaninan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GenreController : ControllerBase // Controller For Genres
    {
        private IGenreRepository _genreRepository; // To Create instance of genre repository

        private readonly IMapper _mapper; // To Create instance of mapper
        private readonly ILogger<GenreController> _logger;   // To Create instance of Logger

        public GenreController(IGenreRepository genreRepository, IMapper mapper , ILogger<GenreController> logger) // Constructor for dependency injection
        {
            _genreRepository = genreRepository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet("{genreId}")]
        public IActionResult GetGenreDetail(int genreId) // To get a genre which is specified by Id column
        {
            _logger.LogInformation("Getting Genre Details");
            var obj = _genreRepository.GetGenre(genreId); // Genre repository to reach database and get specified genre
            if (obj == null)
            {
                throw new KeyNotFoundException("Genre with given Id doesnt Exists"); // If there is no genre in database with given Id
            }
            var objDto = _mapper.Map<GenreDto>(obj); // Mapping Database object to GenreDTO
            return Ok(objDto);
        }
        [HttpGet]
        public IActionResult getGenres() // To get list of genres
        {
            _logger.LogInformation("Getting Genre List");
            var objList = _genreRepository.ListGenres();  // Genre repository to reach database
            if (objList == null)
            {
                throw new KeyNotFoundException("There is no genre which exists"); // If there is no genre in database
            }
            var objDto = new List<GenreDto>();
            foreach (var obj in objList)
            {
                objDto.Add(_mapper.Map<GenreDto>(obj));// Mapping Database object to GenreDTO
            }
            return Ok(objList);
        }
        [HttpPost]
        public IActionResult CreateGenre([FromBody] Genre genre) // To create a new genre
        {
            _logger.LogInformation("New Genre is attempted to create.");
            if (genre == null)
            {
                throw new BadHttpRequestException("Validation Error. Expected paremeters must be given"); // To validate users input
            }
            if (_genreRepository.GenreExists(genre.Id))
            {
                throw new KeyNotFoundException("This Id already exists."); // To check if the given id parameter is already exists in database
            }
            if (!ModelState.IsValid)
            {
                throw new BadHttpRequestException("Validation Error. Expected paremeters must be given"); // To validate users input
            }

            var genreObject = _mapper.Map<Genre>(genre);

           
            if (!_genreRepository.AddGenre(genreObject)) // Genre repository to reach database and add genre object
            {
                throw new Exception("Internal server error.Something Went Wrong while adding Genre. "); // If any internal server error
            }
            return Ok();
        }

        [HttpPut("{genreId:int}")]
        public IActionResult UpdateGenre(int genreId, [FromBody] GenreDto genreDto) // To update already exists genre
        {
            _logger.LogInformation("Genre is attempted to update.");
            if (genreDto == null || genreId != genreDto.Id) // To validate users input and to check if the updated genres Id and Users' given id matches 
            {
                throw new BadHttpRequestException("Validation Error. Expected paremeters must be given");
            }
         /*   var obj = _genreRepository.GetGenre(genreId);
            if (obj == null)
            {
                return NotFound();
            }*/
            var genreObject = _mapper.Map<Genre>(genreDto); 
            if (!_genreRepository.UpdateGenre(genreObject))  // Genre repository to reach database and update genre object
            {
                throw new Exception("Internal server error.Something Went Wrong while updating Genre. "); // If any internal server error
            }

            return NoContent();
        }

        [HttpDelete("{genreId:int}")]
        public IActionResult DeleteGenre(int genreId) // To delete already exists genre
        {
            _logger.LogInformation("Genre is attempted to delete.");
            if (!_genreRepository.GenreExists(genreId))  // Genre repository to reach database and to check if the given id of genre exists
            {
                throw new KeyNotFoundException("This Id doesn't exists."); //If Id doesnt exists
            }
            var genreObject = _genreRepository.GetGenre(genreId);  // Genre repository to reach database and get users selected genre

            if (!_genreRepository.DeleteGenre(genreObject)) // Genre repository to reach database and delete genre object
            {
                throw new Exception("Internal server error.Something Went Wrong while deleting Genre. "); // If any internal server error
            }
            return NoContent();
        }
    }
}
