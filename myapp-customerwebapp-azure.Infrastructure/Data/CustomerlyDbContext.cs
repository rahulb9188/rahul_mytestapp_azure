using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using myapp_customerwebapp_azure.Domain.Entities;
using System;
using System.Collections.Generic;

namespace myapp_customerwebapp_azure.Infrastructure.Data;

public partial class CustomerlyDbContext : DbContext
{
    public CustomerlyDbContext()
    {
    }

    public CustomerlyDbContext(DbContextOptions<CustomerlyDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customerly_Customer> Customerly_Customers { get; set; }

    public virtual DbSet<Customerly_Order> Customerly_Orders { get; set; }

    public virtual DbSet<Customerly_Product> Customerly_Products { get; set; }

    public virtual DbSet<Customerly_Role> Customerly_Roles { get; set; }

    public virtual DbSet<Customerly_Status> Customerly_Statuses { get; set; }

    public virtual DbSet<Customerly_User> Customerly_Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customerly_Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__Customer__A4AE64D8C31B31A4");

            entity.HasIndex(e => e.UserId, "UQ__Customer__1788CC4D291C7F48").IsUnique();

            entity.Property(e => e.CustomerName).HasMaxLength(100);
        });

        modelBuilder.Entity<Customerly_Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Customer__C3905BCF5CD03DAA");

            entity.ToTable("Customerly_Order");

            entity.Property(e => e.OrderDate).HasColumnType("datetime");

            entity.HasOne(d => d.Customer).WithMany(p => p.Customerly_Orders)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__Customerl__Custo__02FC7413");

            entity.HasOne(d => d.Product).WithMany(p => p.Customerly_Orders)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__Customerl__Produ__03F0984C");

            entity.HasOne(d => d.Status).WithMany(p => p.Customerly_Orders)
                .HasForeignKey(d => d.StatusId)
                .HasConstraintName("FK__Customerl__Statu__04E4BC85");
        });

        modelBuilder.Entity<Customerly_Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Customer__B40CC6CD0412BF9B");

            entity.ToTable("Customerly_Product");

            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Customerly_Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Customer__3214EC0732D4DA66");

            entity.Property(e => e.RoleId).ValueGeneratedNever();
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Name)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Customerly_Status>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("PK__Customer__C8EE20631BF81785");

            entity.ToTable("Customerly_Status");

            entity.Property(e => e.StatusId).ValueGeneratedNever();
            entity.Property(e => e.StatusName).HasMaxLength(50);
        });

        modelBuilder.Entity<Customerly_User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Customer__1788CC4CF6461D96");

            entity.HasIndex(e => e.Email, "UQ__Customer__A9D10534B6F62E8E").IsUnique();

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.IsApproved).HasDefaultValue(false);
            entity.Property(e => e.PasswordHash).HasMaxLength(255);

            entity.HasOne(d => d.RoleNavigation).WithMany(p => p.Customerly_Users)
                .HasForeignKey(d => d.Role)
                .HasConstraintName("FK_Roles_Users_RoleID");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
