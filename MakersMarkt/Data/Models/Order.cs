using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakersMarkt.Data.Models
{
    internal class Order
    {
        [Key]
        public int Id { get; set; }
        public int BuyerId { get; set; }
        public int ProductId { get; set; }
        public decimal PricePaid { get; set; }
        public int StatusId { get; set; }
        public string StatusNote { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        [ForeignKey("BuyerId")]
        public User Buyer { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
        [ForeignKey("StatusId")]
        public OrderStatus Status { get; set; }

        // One-to-one relationship with Review
        public Review Review { get; set; }
    }
}
