using AutoMapper;
using Domain;
using Domain.Dto;

namespace Api.Helpers
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<User, UserToCreate>().ReverseMap();
            CreateMap<User, UserToReturn>().ReverseMap();
            CreateMap<User, UserToUpdate>().ReverseMap();

            CreateMap<Expense, ExpenseToReturn>()
            .ForMember(dest => dest.DueDate, opt => opt.MapFrom(src => src.DueDate.ToString("dd/MM/yyyy")));
        }
    }
}