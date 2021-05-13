using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TradeGameCRAPI.Entities;
using TradeGameCRAPI.Interfaces;
using TradeGameCRAPI.Models;

namespace TradeGameCRAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : CustomBaseController<User, UserDTO, UserCreateDTO, UserUpdateDTO>
    {
        private static MapperConfiguration userMapperConfiguration = new MapperConfiguration(cfg => {
            cfg.CreateMap<User, UserDTO>().ReverseMap();
            cfg.CreateMap<UserCreateDTO, User>();
            cfg.CreateMap<UserUpdateDTO, User>();
        });

        public UserController(IRepository<User> UserRepository)
            : base(UserRepository, userMapperConfiguration.CreateMapper()) { }
    }
}
