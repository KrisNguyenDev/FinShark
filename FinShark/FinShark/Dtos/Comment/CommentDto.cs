using FinShark.Mappings;

namespace FinShark.Dtos.Comment
{
    public class CommentDto : IMapFrom<Model.Comment>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedOn { get; set; }
        public string? AppUserId { get; set; }
    }
}
