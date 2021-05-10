using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TradeGameCRAPI.Entities;
using TradeGameCRAPI.Models;
using TradeGameCRAPI.Repositories;
using TradeGameCRAPI.Services;

namespace TradeGameCRAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : AppBaseController<User, UserDTO, UserCreateDTO, UserUpdateDTO, UserRepository>
    {
        public UserController(IUserService userService) : base(userService) { }
    }
}
