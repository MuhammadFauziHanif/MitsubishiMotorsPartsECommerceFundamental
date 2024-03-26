using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MitsubishiMotorsPartsECommerce.Domain.Models;

[Keyless]
public partial class PendingOrderPayment
{
    [Column("OrderID")]
    public int OrderId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime OrderDate { get; set; }

    [Column(TypeName = "money")]
    public decimal TotalAmount { get; set; }

    [StringLength(20)]
    public string OrderStatus { get; set; } = null!;

    [StringLength(50)]
    public string CustomerFirstName { get; set; } = null!;

    [StringLength(50)]
    public string CustomerLastName { get; set; } = null!;

    [StringLength(100)]
    public string CustomerEmail { get; set; } = null!;

    [StringLength(20)]
    public string CustomerPhoneNumber { get; set; } = null!;

    public string CustomerAddress { get; set; } = null!;

    [Column("ProductID")]
    public int ProductId { get; set; }

    [StringLength(100)]
    public string ProductName { get; set; } = null!;

    public int Quantity { get; set; }

    [Column(TypeName = "money")]
    public decimal UnitPricePerItem { get; set; }

    [Column(TypeName = "money")]
    public decimal? TotalPrice { get; set; }

    [Column("PaymentID")]
    public int? PaymentId { get; set; }

    [Column(TypeName = "money")]
    public decimal? PaymentAmount { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? PaymentDate { get; set; }

    [StringLength(50)]
    public string? PaymentMethod { get; set; }

    [StringLength(20)]
    public string? PaymentStatus { get; set; }
}
