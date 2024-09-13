namespace Musicians_Pocket_Knife.Profiles
{
    using AutoMapper;
    using Musicians_Pocket_Knife.Models;

    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<CreateNewUserRequest, User>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.GoogleId, opt => opt.MapFrom(src => src.Id));
        }
    }
}
