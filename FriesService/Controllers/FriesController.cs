using FriesService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderLibrary.Models;

namespace FriesService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FriesController : ControllerBase
    {
        private readonly DbContextSettings _context;
        public FriesController(DbContextSettings context)
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
