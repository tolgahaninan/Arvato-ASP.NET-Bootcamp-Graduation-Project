using AutoMapper;
using GradBootcamp_Tolgahaninan.Middlewares;
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

    public class MovieController : ControllerBase // Controller For Movies
    {

        private IMovieRepository _movieRepository;// To Create instance of movie repository
        private IMovieViewRepository _movieViewRepository; // To Create instance of movieview repository
        private readonly IMapper _mapper; // To Create instance of mapper
        private readonly ILogger<MovieController> _logger; // To Create instance of Logger

        public MovieController(IMovieRepository movieRepository, IMapper mapper, ILogger<MovieController> logger , IMovieViewRepository movieViewRepository) // Constructor for dependency injection
        {
            _movieRepository = movieRepository;
            _movieViewRepository = movieViewRepository;
            _mapper = mapper;
            _logger= logger;
        }

        [HttpGet("{movieId}")]
        public IActionResult GetMovieDetail(int movieId) // To get a movie which is specified by Id column
        {
            _logger.LogInformation("Getting Movie Details");
            var obj = _movieRepository.GetMovieDetail(movieId); // Movie repository to reach database and get specified movie
            if (obj == null)
            {
                throw new KeyNotFoundException("Movie with given Id doesnt Exists"); // If there is no movie in database with given Id
            }

            var movieViewObj = _movieViewRepository.GetMovieViewDetail(movieId);  // MovieView repository to reach database and get specified movieview of given movie id

            if (movieViewObj!= null) // If Movie with this id is already clicked
            {
                movieViewObj.ClickCounter++; // Incremantation of click counter of movie
                _movieViewRepository.IncrementMovieView(movieViewObj);     

            }
            else // If Movie with this id hasn't been clicked yet
            {
                if (_movieViewRepository.MovieViewExists(movieId)) // To Double check if the movie is clicked
                {
                    throw new KeyNotFoundException("This Id already exists."); 
                }

                _movieViewRepository.AddMovieView(new MovieView() // To Add new movieview with the click counter 1
                {
                    movie = obj,
                    ClickCounter = 1

                });

            }

           var objDto = _mapper.Map<MovieDto>(obj);//  // Mapping Database object to MovieDto
            return Ok(objDto);
        }
        
          [HttpGet("[action]/{genreId:int}")]
                public IActionResult GetMovieListByGenres(int genreId) // To get list of Movies by genres This section couldn't be completed
        {
                    var obj = _movieRepository.GetMovieListByGenreId(genreId);
                    if (obj == null)
                    {
                        return NotFound();
                    }
                   // var objDto = _mapper.Map<MovieDto>(obj);
                    return Ok(obj);
                }
        
        [HttpGet("[action]/{rateFilter:decimal}")]
        public IActionResult GetMovieListByRates(decimal rateFilter) // To get list of Movies by rate filter
        {
            _logger.LogInformation("Getting Movie List By Rates");
            var objList = _movieRepository.GetMovieListByRateFilter(rateFilter); // Movie repository to reach database and get specified movies by rate filter

            if (!objList.Any()) // If there is no movie with given rate filter
            {
                throw new KeyNotFoundException("Movie with given rate paremeters doesn't exists");
            }
            var objDto = new List<MovieDto>(); 
            foreach (var obj in objList)
            {
                objDto.Add(_mapper.Map<MovieDto>(obj)); // Mapping Database object to MovieDto
            }
            return Ok(objDto);
        }

        [HttpGet("[action]/{releaseDate:datetime}")]
        public IActionResult GetMovieListByReleaseDate(string releaseDate) // To get list of Movies by release date
        {
            _logger.LogInformation("Getting Movie List By Release Date");
            var objList = _movieRepository.GetMovieListByReleaseDate(releaseDate); // Movie repository to reach database and get specified movies by release date filter

            if (!objList.Any()) // If there is no movie with given release date
            {
                throw new KeyNotFoundException("Movie with given release date paremeters doesn't exists");
            }
            if (!ModelState.IsValid) // If the given paremeter by user is valid
            {
                throw new BadHttpRequestException("Validation Error. Expected paremeters must be given");
            }
            var objDto = new List<MovieDto>();
            foreach (var obj in objList)
            {
                objDto.Add(_mapper.Map<MovieDto>(obj));// Mapping Database object to MovieDto
            }
            return Ok(objDto);
        }
        [HttpPost]
        public IActionResult CreateMovie([FromBody] Movie movieDto) // To create a new movie
        {
            _logger.LogInformation("New Movie is attempted to create.");
            if (movieDto == null)
            {
                throw new BadHttpRequestException("Validation Error. Expected paremeters must be given"); // To validate users input
            }
            if (_movieRepository.MovieExists((int)movieDto.Id))
            {
                throw new KeyNotFoundException("This Id already exists."); // To check if the given id parameter is already exists in database
            }
            if (!ModelState.IsValid)
            {
                throw new BadHttpRequestException("Validation Error. Expected paremeters must be given");  // To validate users input
            }

            var movieObj = _mapper.Map<Movie>(movieDto);

            if (!_movieRepository.AddMovie(movieObj)) // Movie repository to reach database and add movie object
            {
               throw new Exception("Internal server error.Something Went Wrong while adding Movie. "); // If any internal server error
            }
            return Ok();
        }

        [HttpPut("{movieId:int}")]
        public IActionResult UpdateMovie(int movieId, [FromBody] MovieDto movieDto) // To update already exists movie
        {
            _logger.LogInformation("Movie is attempted to update.");
            if (movieDto == null || movieId != movieDto.Id) // To validate users input and to check if the updated genres Id and Users' given id matches 
            {
                throw new BadHttpRequestException("Validation Error. Expected paremeters must be given and given movie Id must be matched with updated movie Id");
            }
            var movieObject = _mapper.Map<Movie>(movieDto);
            if (!_movieRepository.UpdateMovie(movieObject))// Movie repository to reach database and update movie object
            {
                throw new Exception("Internal server error.Something Went Wrong while updating Movie. "); // If any internal server error
            }

            return NoContent();
        }

        [HttpDelete("{movieId:int}")]
        public IActionResult DeleteMovie(int movieId) // To delete already exists movie
        {
            _logger.LogInformation("Movie is attempted to delete.");
            if (!_movieRepository.MovieExists(movieId))  // Movie repository to reach database and to check if the given id of movie exists
            {
                throw new KeyNotFoundException("This Id doesn't exists."); //If Id doesnt exists
            }
            var movieObject = _movieRepository.GetMovieDetail(movieId); // Movie repository to reach database and get users selected movie

            if (!_movieRepository.DeleteMovie(movieObject)) // Movie repository to reach database and delete selected movie object
            {
                throw new Exception("Internal server error.Something Went Wrong while deleting Movie. "); // If any internal server error
            }
            return NoContent();
        }


        [HttpGet("[action]")]
        public IActionResult SearchMovie(string? title , decimal? rate , string? year) // To search movie with given paremeters
        {
            _logger.LogInformation("Movie is searched.");
            var objList = _movieRepository.SearchMovie(title,rate,year); // Movie repository to reach database and to check if the movie with given parameters exists

            if (!objList.Any()) // If there is no movie with given paremeters
            {
                throw new KeyNotFoundException("Movie with given paremeters doesn't exists.");
            }
            var objDto = new List<MovieDto>();
            foreach (var obj in objList)
            {
                objDto.Add(_mapper.Map<MovieDto>(obj));// Mapping Database object to MovieDto
            }
           
            return Ok(objDto);
        }


    }
}
