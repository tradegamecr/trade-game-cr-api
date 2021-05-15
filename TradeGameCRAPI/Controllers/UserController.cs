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
        public UserController(IRepository<User> UserRepository) : base(UserRepository) {}
    }
}
