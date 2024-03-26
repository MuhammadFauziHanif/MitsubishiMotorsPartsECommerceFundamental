using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MitsubishiMotorsPartsECommerce.Domain.Models;

[Table("Product")]
[Index("ProductName", Name = "UQ__Product__DD5A978A5F6A8A38", IsUnique = true)]
public partial class Product
{
    [Key]
    [Column("ProductID")]
    public int ProductId { get; set; }

    [StringLength(100)]
    public string ProductName { get; set; } = null!;

    [Column("CategoryID")]
    public int CategoryId { get; set; }

    public string Description { get; set; } = null!;

    [Column(TypeName = "money")]
    public decimal Price { get; set; }

    public int StockQuantity { get; set; }

    public string ImageUrl { get; set; } = null!;

    [ForeignKey("CategoryId")]
    [InverseProperty("Products")]
    public virtual ProductCategory Category { get; set; } = null!;

    [InverseProperty("Product")]
    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
