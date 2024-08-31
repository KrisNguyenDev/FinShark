using FinShark.Dtos.Comment;
using FinShark.Mappings;

namespace FinShark.Dtos.Stock
{
    public class StockDto : IMapFrom<Model.Stock>
    {
        public int Id { get; set; }
        public string? Symbol { get; set; }
        public string? CompanyName { get; set; }
        public decimal Purchase { get; set; }
        public decimal LastDiv { get; set; }
        public string? Industry { get; set; }
        public long MarketCap { get; set; }
        public List<CommentDto>? Comments { get; set; }
    }
}
