using AutoMapper;
using FinShark.Data;
using FinShark.Dtos.Stock;
using FinShark.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinShark.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IMapper _mapper;

        public StockController(ApplicationDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() 
        {
            var stocks = await _context.Stock.ToListAsync();
            return Ok(_mapper.Map<List<StockDto>>(stocks));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id) 
        {
            var stock = await _context.Stock.FirstOrDefaultAsync(x => x.Id == id);

            if(stock is null) 
            {
                return Problem(statusCode: StatusCodes.Status404NotFound, title: "Stock not found.");
            }

            return Ok(_mapper.Map<StockDto>(stock));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var result = await _context.Stock
                .Where(x => x.Id == id)
                .ExecuteDeleteAsync();

            if(result == 0)
            {
                return BadRequest();
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockDto request)
        {
            Stock stock = _mapper.Map<Stock>(request);

            await _context.Stock.AddAsync(stock);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = stock.Id }, stock);
        }
    }
}
