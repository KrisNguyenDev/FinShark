using FinShark.Helper;
using FinShark.Model;

namespace FinShark.Interfaces
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllAsync(QueryObject queryObject);
        Task<Stock> GetByIdAsync(int id);
        Task<Stock> CreateAsync(Stock stock);
        Task<int> UpdateAsync(int id, Stock stock);
        Task<int> DeleteAsync(int id);
    }
}
