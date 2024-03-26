using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MitsubishiMotorsPartsECommerce.Domain.Models;

[Keyless]
public partial class CategorySalesView
{
    [StringLength(50)]
    public string CategoryName { get; set; } = null!;

    [Column(TypeName = "money")]
    public decimal TotalSalesAmount { get; set; }
}
