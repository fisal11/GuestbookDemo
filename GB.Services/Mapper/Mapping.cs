using AutoMapper;
using GB.DataAccess.Entities;
using GB.Services.Model;

namespace GB.Services.Mapper
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Message, MessageModel>().ReverseMap();

        }
    }
}
