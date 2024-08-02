using Azure.Core;
using FinShark.Data;
using FinShark.Helper;
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

        public async Task<List<Stock>> GetAllAsync(QueryObject queryObject)
        {
            var stocks = _context.Stock.Include(x => x.Comments).AsQueryable();

            if (!string.IsNullOrWhiteSpace(queryObject.CompanyName))
            {
                stocks = stocks.Where(x => x.CompanyName.Trim().ToLower().Contains(queryObject.CompanyName.Trim().ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(queryObject.Symbol))
            {
                stocks = stocks.Where(x => x.Symbol.Trim().ToLower().Contains(queryObject.Symbol.Trim().ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(queryObject.SortBy))
            {
                if (queryObject.SortBy.Equals("Symbol"))
                {
                    stocks = queryObject.IsDecsending ? stocks.OrderByDescending(x => x.Symbol) : stocks.OrderBy(x => x.Symbol);
                }

                if (queryObject.SortBy.Equals("CompanyName"))
                {
                    stocks = queryObject.IsDecsending ? stocks.OrderByDescending(x => x.CompanyName) : stocks.OrderBy(x => x.CompanyName);
                }
            }

            return await stocks.Skip((queryObject.PageNumber - 1) * queryObject.PageSize).Take(queryObject.PageSize).ToListAsync();
        }

        public async Task<Stock> GetByIdAsync(int id)
        {
            var stock = await _context.Stock.Include(x => x.Comments).FirstOrDefaultAsync(x => x.Id == id);
            return stock;
        }

        public async Task<int> UpdateAsync(int id, Stock stock)
        {
            var result = await _context.Stock
                .Where(x => x.Id == id)
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
