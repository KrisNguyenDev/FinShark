using System.ComponentModel.DataAnnotations;

namespace FinShark.Dtos.Stock
{
    public class UpdateStockRequest
    {
        [Required]
        public int Id { get; set; }
        public string Symbol { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;

        [Range(0, 100000000, ErrorMessage = "Khoảng 0 đến 100000000")]
        public decimal Purchase { get; set; }

        [Range(0, 100000000, ErrorMessage = "Khoảng 0 đến 100000000")]
        public decimal LastDiv { get; set; }
        public string Industry { get; set; } = string.Empty;
        public long MarketCap { get; set; }
    }
}
