using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeGameCRAPI.Entities;
using TradeGameCRAPI.Models;
using TradeGameCRAPI.Repositories;

namespace TradeGameCRAPI.Services
{
    public class UserService : BaseControllerService<User, UserDTO, UserCreateDTO, UserUpdateDTO, UserRepository>, IUserService
    {
        private static MapperConfiguration userMapperConfiguration = new MapperConfiguration(cfg => {
            cfg.CreateMap<User, UserDTO>().ReverseMap();
            cfg.CreateMap<UserCreateDTO, User>();
            cfg.CreateMap<UserUpdateDTO, User>();
        });

        public UserService(UserRepository UserRepository)
            : base(UserRepository, UserService.userMapperConfiguration.CreateMapper()) { }
    }
}
