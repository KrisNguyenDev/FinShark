using AutoMapper;
using FinShark.Data;
using FinShark.Dtos.Stock;
using FinShark.Interfaces;
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
        private readonly IStockRepository _stockRepository;
        private readonly IMapper _mapper;

        public StockController(ApplicationDBContext context,IStockRepository stockRepository, IMapper mapper)
        {
            _context = context;
            _stockRepository = stockRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var stocks = await _stockRepository.GetAllAsync();
            return Ok(_mapper.Map<List<StockDto>>(stocks));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var stock = await _stockRepository.GetByIdAsync(id);

            if (stock is null)
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

            if (result == 0)
            {
                return BadRequest();
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockRequestDto request)
        {
            Stock stock = new Stock
            {
                CompanyName = request.CompanyName,
                Industry = request.Industry,
                LastDiv = request.LastDiv,
                MarketCap = request.MarketCap,
                Purchase = request.Purchase,
                Symbol = request.Symbol
            };

            await _stockRepository.CreateAsync(stock);

            return CreatedAtAction(nameof(GetById), new { id = stock.Id }, stock);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto request)
        {
            var stock = await _context.Stock.FirstOrDefaultAsync(x=>x.Id == id);

            if(stock == null)
            {
                return NotFound();
            }

            Stock stockEntity = new Stock
            {
                Id = stock.Id,
                Symbol = request.Symbol,
                CompanyName =request.CompanyName,
                Industry = request.Industry,
                LastDiv = request.LastDiv,
                MarketCap = request.MarketCap,
                Purchase= request.Purchase,
            };

            await _stockRepository.UpdateAsync(id, stockEntity);
            return Ok(stockEntity);
        }
    }
}
