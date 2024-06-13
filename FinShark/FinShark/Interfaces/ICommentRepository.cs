using FinShark.Model;

namespace FinShark.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllAsync();
        Task<Comment> GetByIdAsync(int id);
        Task<Comment> CreateAsync(Comment comment);
        Task<int> UpdateAsync(int id, Comment comment);
        Task<int> DeleteAsync(int Id);
    }
}
