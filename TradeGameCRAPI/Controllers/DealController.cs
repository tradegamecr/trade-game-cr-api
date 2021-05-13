using AutoMapper;
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
        private static MapperConfiguration dealMapperConfiguration = new MapperConfiguration(cfg => {
            cfg.CreateMap<Deal, DealDTO>().ReverseMap();
            cfg.CreateMap<DealCreateDTO, Deal>();
            cfg.CreateMap<DealUpdateDTO, Deal>();
        });

        public DealController(IRepository<Deal> DealRepository)
            : base(DealRepository, dealMapperConfiguration.CreateMapper()) { }
    }
}
