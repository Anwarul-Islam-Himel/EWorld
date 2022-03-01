using Microsoft.AspNetCore.Mvc;
using Service.Services;
using Service.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_World.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopController : ControllerBase
    {
        private readonly IShopService _shopService;
        public ShopController(IShopService shopService)
        {
            _shopService = shopService;
        }
        [HttpPost]
        public async Task<IActionResult> AddShop(ShopVM model)
        {
            var response = await _shopService.AddShopAsync(model);
            if(response == null)
            {
                return BadRequest();
            }
            return Ok(response);
        }
        [HttpGet]
        public async Task<IActionResult> GetShops() => Ok(await _shopService.GetShopsAsync());
    }
}
