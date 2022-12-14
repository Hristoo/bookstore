using AutoMapper;
using BookStore.Models.Models;
using BookStore.Models.Models.Requests;

namespace BookStore2.AutoMapper
{
    internal class AutoMappings : Profile
    {
        public AutoMappings()
        {
            CreateMap<AddAuthorRequest, Author>();
            CreateMap<UpdateAuthorRequest, Author>();
            CreateMap<AddBookRequest, Book>();
        }
    }
}
