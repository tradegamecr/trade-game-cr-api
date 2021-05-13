using AutoMapper;
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
        private static MapperConfiguration postMapperConfiguration = new MapperConfiguration(cfg => {
            cfg.CreateMap<Post, PostDTO>().ReverseMap();
            cfg.CreateMap<PostCreateDTO, Post>();
            cfg.CreateMap<PostUpdateDTO, Post>();
        });

        public PostController(IRepository<Post> PostRepository)
            : base(PostRepository, postMapperConfiguration.CreateMapper()) { }
    }
}
