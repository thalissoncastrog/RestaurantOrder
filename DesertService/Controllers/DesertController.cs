using DesertService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DesertService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DesertController : ControllerBase
    {
        private readonly DbContextSettings _context;
        public DesertController(DbContextSettings context)
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
