using AutoMapper;
using TradeGameCRAPI.Entities;
using TradeGameCRAPI.Interfaces;
using TradeGameCRAPI.Models;
using TradeGameCRAPI.Repositories;

namespace TradeGameCRAPI.Services
{
    public class PostService
        : BaseControllerService<Post, PostDTO, PostCreateDTO, PostUpdateDTO, PostRepository>,
        IBaseControllerService<Post, PostDTO, PostCreateDTO, PostUpdateDTO>
    {
        private static MapperConfiguration postMapperConfiguration = new MapperConfiguration(cfg => {
            cfg.CreateMap<Post, PostDTO>().ReverseMap();
            cfg.CreateMap<PostCreateDTO, Post>();
            cfg.CreateMap<PostUpdateDTO, Post>();
        });

        public PostService(PostRepository PostRepository)
            : base(PostRepository, postMapperConfiguration.CreateMapper()) { }
    }
}
