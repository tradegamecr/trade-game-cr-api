using AutoMapper;
using TradeGameCRAPI.Entities;
using TradeGameCRAPI.Interfaces;
using TradeGameCRAPI.Models;
using TradeGameCRAPI.Repositories;

namespace TradeGameCRAPI.Services
{
    public class UserService
        : BaseControllerService<User, UserDTO, UserCreateDTO, UserUpdateDTO, UserRepository>,
        IBaseControllerService<User, UserDTO, UserCreateDTO, UserUpdateDTO>
    {
        private static MapperConfiguration userMapperConfiguration = new MapperConfiguration(cfg => {
            cfg.CreateMap<User, UserDTO>().ReverseMap();
            cfg.CreateMap<UserCreateDTO, User>();
            cfg.CreateMap<UserUpdateDTO, User>();
        });

        public UserService(UserRepository UserRepository)
            : base(UserRepository, userMapperConfiguration.CreateMapper()) { }
    }
}
