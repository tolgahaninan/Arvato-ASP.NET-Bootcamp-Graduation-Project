using AutoMapper;
using GradBootcamp_Tolgahaninan.Models;
using GradBootcamp_Tolgahaninan.Models.Dtos;
namespace GradBootcamp_Tolgahaninan.Mapper
{
    public class Mappings : Profile // Mapper
    {
        public Mappings()
        {
            CreateMap<User, UserDto>().ReverseMap(); // To map User to UserDto and reverse too
            CreateMap<Movie, MovieDto>().ReverseMap(); // To map Movie to MovieDto and reverse too
            CreateMap<Genre, GenreDto>().ReverseMap(); // To map Genre to GenreDto and reverse too
            CreateMap<MovieView, MovieViewDto>().ReverseMap(); // To map MovieView to MovieViewDto and reverse too
        }
    }
}
