using AutoMapper;
using Book_DirectorApplication.Models.DTOS;
using Book_DirectorApplication.Models.Entities;
using System.IO;

namespace Book_DirectorApplication.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Author, AuthorDTO>().ReverseMap();
            CreateMap<Book, BookDTO>().ReverseMap();
        }
    }
}
