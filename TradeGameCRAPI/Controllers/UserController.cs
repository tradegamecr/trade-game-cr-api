using Microsoft.AspNetCore.Mvc;
using TradeGameCRAPI.Entities;
using TradeGameCRAPI.Models;
using TradeGameCRAPI.Services;

namespace TradeGameCRAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : AppBaseController<User, UserDTO, UserCreateDTO, UserUpdateDTO, UserService>
    {
        public UserController(UserService UserService) : base(UserService) { }
    }
}
