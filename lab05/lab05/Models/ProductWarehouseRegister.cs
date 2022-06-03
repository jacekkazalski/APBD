using System.ComponentModel.DataAnnotations;

namespace lab05.Models
{
    public class ProductWarehouseRegister
    {
        [Required]
        public int IdProduct { get; set; }
        [Required]
        public int IdWarehouse { get; set; }
        [Range(1,int.MaxValue)]
        public int Amount { get; set; }
        [Required]
        public string CreatedAt { get; set; }
    }
}
