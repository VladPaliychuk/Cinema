using AutoMapper;

namespace User.BLL.Configurations;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateUserMaps();
    }
    
    private void CreateUserMaps()
    {
        CreateMap<DTOs.UserDto, DAL.Entities.User>();
        CreateMap<DAL.Entities.User, DTOs.UserDto>();
    }
}