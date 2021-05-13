using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TradeGameCRAPI.Entities;
using TradeGameCRAPI.Interfaces;
using TradeGameCRAPI.Models;

namespace TradeGameCRAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : CustomBaseController<Product, ProductDTO, ProductCreateDTO, ProductUpdateDTO>
    {
        private static MapperConfiguration productMapperConfiguration = new MapperConfiguration(cfg => {
            cfg.CreateMap<Product, ProductDTO>().ReverseMap();
            cfg.CreateMap<ProductCreateDTO, Product>();
            cfg.CreateMap<ProductUpdateDTO, Product>();
        });

        public ProductController(IRepository<Product> ProductRepository)
            : base(ProductRepository, productMapperConfiguration.CreateMapper()) { }
    }
}
