using System.ComponentModel.DataAnnotations;

namespace FinShark.Dtos.Comment
{
    public class CreateCommentRequest
    {
        [Required]
        [MinLength(5, ErrorMessage = "Tiêu đề phải lớn hơn 5 ký tự.")]
        [MaxLength(280, ErrorMessage = "Tiêu đề không thể vượt quá 280 ký tự.")]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MinLength(5, ErrorMessage = "Nội dung phải lớn hơn 5 ký tự.")]
        [MaxLength(280, ErrorMessage = "Nội dung không thể vượt quá 280 ký tự.")]
        public string Content { get; set; } = string.Empty;
        public int StockId { get; set; }
    }
}
