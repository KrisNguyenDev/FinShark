using Azure.Core;
using FinShark.Data;
using FinShark.Interfaces;
using FinShark.Model;
using Microsoft.EntityFrameworkCore;

namespace FinShark.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDBContext _context;

        public StockRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Stock> CreateAsync(Stock stock)
        {
            await _context.AddAsync(stock);
            await _context.SaveChangesAsync();
            return stock;
        }

        public async Task<int> DeleteAsync(int id)
        {
            var result = await _context.Stock
                .Where(x => x.Id == id)
                .ExecuteDeleteAsync();

            return result;
        }

        public async Task<List<Stock>> GetAllAsync()
        {
            var stocks = await _context.Stock.ToListAsync();
            return stocks;
        }

        public async Task<Stock> GetByIdAsync(int id)
        {
            var stock = await _context.Stock.FirstOrDefaultAsync(x => x.Id == id);
            return stock;
        }

        public async Task<int> UpdateAsync(int id, Stock stock)
        {
            var result = await _context.Stock
                .Where (x => x.Id == id)
                .ExecuteUpdateAsync(setter => setter
                .SetProperty(x => x.Symbol, stock.Symbol)
                .SetProperty(x => x.CompanyName, stock.CompanyName)
                .SetProperty(x => x.LastDiv, stock.LastDiv)
                .SetProperty(x => x.Industry, stock.Industry)
                .SetProperty(x => x.MarketCap, stock.MarketCap)
                );
            return result;
        }
    }
}
