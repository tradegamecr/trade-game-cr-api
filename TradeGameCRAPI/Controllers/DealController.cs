using Microsoft.AspNetCore.Mvc;
using TradeGameCRAPI.Entities;
using TradeGameCRAPI.Interfaces;
using TradeGameCRAPI.Models;

namespace TradeGameCRAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DealController : CustomBaseController<Deal, DealDTO, DealCreateDTO, DealUpdateDTO>
    {
        public DealController(IRepository<Deal> DealRepository) : base(DealRepository) { }
    }
}
