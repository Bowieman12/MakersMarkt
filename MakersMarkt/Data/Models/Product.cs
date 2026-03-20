using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakersMarkt.Data.Models
{
    internal class Product
    {
        [Key]
        public int Id { get; set; }
        public int MakerId { get; set; }
        public int CategoryId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Materials { get; set; }
        public int ProductionTimeDays { get; set; }
        public int ComplexityLevelId { get; set; }
        public string DurabilityInfo { get; set; }
        public string UniqueFeatures { get; set; }
        public bool IsApproved { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        [ForeignKey("MakerId")]
        public User Maker { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
        [ForeignKey("ComplexityLevelId")]
        public ComplexityLevel ComplexityLevel { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
