using AutoMapper;
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
    public class TrendController : ControllerBase // Controller For Trends
    {
        private ITrendRepository _trendRepository; // To Create instance of movie repository

        private readonly IMapper _mapper;// To Create instance of mapper

        private readonly ILogger<TrendController> _logger;// To Create instance of Logger
        public TrendController(ITrendRepository trendRepository, IMapper mapper, ILogger<TrendController> logger) // Constructor for dependency injection
        {
            _trendRepository = trendRepository;
            _mapper = mapper;
            _logger = logger;
        }
        [HttpGet("[action]")]
        public IActionResult ListTopRatedMovies() // To get Top rated movies by their vote average
        {
            _logger.LogInformation("Getting Top Rated Movies");
            var objList = _trendRepository.ListTopRatedMovies(); // Trend repository to reach database and get top rated movies
            var objDto = new List<MovieDto>();
            if (objList == null) // If there is no movie in database
            {
                throw new KeyNotFoundException("There is no movie found.");
            }
            foreach (var obj in objList)
            {
                objDto.Add(_mapper.Map<MovieDto>(obj)); // Mapping Database object to MovieDto
            }
        
            return Ok(objDto);
        }

        [HttpGet("[action]")]
        public IActionResult ListTopViewedMovies() // To get Top rated movies by their click counters
        {
            _logger.LogInformation("Getting Top Viewed Movies");
            var objList = _trendRepository.ListMostViewedMovies(); // Trend repository to reach database and get most clickled movies
       
            if (objList == null) // If there is no clicked movie in database
            {
                throw new KeyNotFoundException("There is no Movie - view data is not found.");
            }
        /*    foreach (var obj in objList)
            {
                objDto.Add(_mapper.Map<MovieViewDto>(obj));
            }*/

            return Ok(objList);
        }
    }
}
