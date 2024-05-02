using AutoMapper;

namespace UserCard.BLL.Configuration;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateUserCardMaps();
    }
    
    private void CreateUserCardMaps()
    {
        CreateMap<DTOs.UserCardDto, DAL.Entities.UserCard>();
        CreateMap<DAL.Entities.UserCard, DTOs.UserCardDto>();
    }
}