using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using MitsubishiMotorsPartsECommerce.Domain.Models;


namespace MitsubishiMotorsPartsECommerce.Data;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<CategorySalesView> CategorySalesViews { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<OrderHeader> OrderHeaders { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<PendingOrderPayment> PendingOrderPayments { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductCategory> ProductCategories { get; set; }

    public virtual DbSet<TotalRevenuePerMonth> TotalRevenuePerMonths { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasKey(e => e.Username).HasName("PK__Admin__536C85E5FE3BBB4B");

            entity.Property(e => e.Role)
                .HasDefaultValue("admin")
                .IsFixedLength();
        });

        modelBuilder.Entity<CategorySalesView>(entity =>
        {
            entity.ToView("CategorySalesView");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__Customer__A4AE64B89629957C");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => e.OrderDetailId).HasName("PK__OrderDet__D3B9D30CE7702350");

            entity.Property(e => e.Quantity).HasDefaultValue(1);

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails).HasConstraintName("FK__OrderDeta__Order__48CFD27E");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderDetails).HasConstraintName("FK__OrderDeta__Produ__49C3F6B7");
        });

        modelBuilder.Entity<OrderHeader>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__OrderHea__C3905BAFDAE8B63C");

            entity.Property(e => e.OrderDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Customer).WithMany(p => p.OrderHeaders).HasConstraintName("FK__OrderHead__Custo__4316F928");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__Payment__9B556A589D479EA9");

            entity.Property(e => e.PaymentDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Order).WithMany(p => p.Payments).HasConstraintName("FK__Payment__OrderID__4E88ABD4");
        });

        modelBuilder.Entity<PendingOrderPayment>(entity =>
        {
            entity.ToView("PendingOrderPayment");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Product__B40CC6ED46B483A3");

            entity.HasOne(d => d.Category).WithMany(p => p.Products).HasConstraintName("FK__Product__Categor__3A81B327");
        });

        modelBuilder.Entity<ProductCategory>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__ProductC__19093A2B051C4879");
        });

        modelBuilder.Entity<TotalRevenuePerMonth>(entity =>
        {
            entity.ToView("TotalRevenuePerMonth");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
