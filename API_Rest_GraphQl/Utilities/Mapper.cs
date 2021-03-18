using API_Rest_GraphQl.Models;
using API_Rest_GraphQl.Models.DTOs;
using AutoMapper;

namespace API_Rest_GraphQl.Utilities
{
    public class Mapper
    {
        public IMapper mapper;
        public Mapper()
        {
            var configuration = new MapperConfiguration(configuration =>
            {
                configuration.CreateMap<Livro, LivroDTO>();
                configuration.CreateMap<LivroDTO, Livro>();
            });

            mapper = configuration.CreateMapper();
        }
    }
}
