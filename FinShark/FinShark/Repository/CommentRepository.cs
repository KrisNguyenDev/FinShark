using FinShark.Data;
using FinShark.Interfaces;
using FinShark.Model;
using Microsoft.EntityFrameworkCore;

namespace FinShark.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDBContext _context;

        public CommentRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Comment> CreateAsync(Comment comment)
        {
            await _context.Comment.AddAsync(comment);
            await _context.SaveChangesAsync();

            return comment;
        }

        public async Task<int> DeleteAsync(int id)
        {
            return await _context.Comment
                .Where(x => x.Id == id)
                .ExecuteDeleteAsync();
        }

        public async Task<List<Comment>> GetAllAsync()
        {
            var comments = await _context.Comment.ToListAsync();
            return comments;
        }

        public async Task<Comment> GetByIdAsync(int id)
        {
            var comment = await _context.Comment.FirstOrDefaultAsync(x => x.Id == id);
            return comment ?? new Comment(); // Return a default Comment if comment is null
        }

        public async Task<int> UpdateAsync(int id, Comment comment)
        {
            return await _context.Comment
                .Where(x => x.Id == id)
                .ExecuteUpdateAsync(setter => setter
                    .SetProperty(x => x.Title, comment.Title)
                    .SetProperty(x => x.Content, comment.Content)
                );
        }
    }
}
