using Microsoft.AspNetCore.Mvc;
using TradeGameCRAPI.Entities;
using TradeGameCRAPI.Interfaces;
using TradeGameCRAPI.Models;

namespace TradeGameCRAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : CustomBaseController<Post, PostDTO, PostCreateDTO, PostUpdateDTO>
    {
        public PostController(IRepository<Post> PostRepository) : base(PostRepository) { }
    }
}
