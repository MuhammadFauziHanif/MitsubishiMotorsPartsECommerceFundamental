using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MitsubishiMotorsPartsECommerce.Domain.Models;

[Keyless]
public partial class TotalRevenuePerMonth
{
    public int? OrderYear { get; set; }

    public int? OrderMonth { get; set; }

    [Column(TypeName = "money")]
    public decimal? TotalRevenue { get; set; }
}
