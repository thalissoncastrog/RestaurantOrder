using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaladService.Models;

namespace SaladService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SaladController : ControllerBase
    {
        private readonly DbContextSettings _context;
        public SaladController(DbContextSettings context)
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
