using DrinkService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DrinkService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DrinkController : ControllerBase
    {
        private readonly DbContextSettings _context;
        public DrinkController(DbContextSettings context)
        {

            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            return await _context.Orders.ToArrayAsync();
        }
    }
}
