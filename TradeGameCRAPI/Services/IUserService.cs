using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeGameCRAPI.Entities;
using TradeGameCRAPI.Models;
using TradeGameCRAPI.Repositories;

namespace TradeGameCRAPI.Services
{
    public interface IUserService : IBaseControllerService<User, UserDTO, UserCreateDTO, UserUpdateDTO, UserRepository>
    {

    }
}
