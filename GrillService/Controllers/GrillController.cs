using GrillService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GrillService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GrillController : ControllerBase
    {
        private readonly DbContextSettings _context;
        public GrillController(DbContextSettings context)
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
