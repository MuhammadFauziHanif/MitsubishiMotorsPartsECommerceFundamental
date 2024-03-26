using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MitsubishiMotorsPartsECommerce.Domain.Models;

[Table("Admin")]
[Index("Email", Name = "UQ__Admin__A9D105345A61B17A", IsUnique = true)]
public partial class Admin
{
    [Key]
    [StringLength(50)]
    public string Username { get; set; } = null!;

    [StringLength(100)]
    public string Email { get; set; } = null!;

    [StringLength(128)]
    public string Password { get; set; } = null!;

    [StringLength(10)]
    public string Role { get; set; } = null!;
}
