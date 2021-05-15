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
        public ProductController(IRepository<Product> ProductRepository) : base(ProductRepository) { }
    }
}
