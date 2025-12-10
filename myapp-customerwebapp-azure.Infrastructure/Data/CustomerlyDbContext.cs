using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using myapp_customerwebapp_azure.Infrastructure;

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

    public virtual DbSet<Customerly_UserRole> Customerly_UserRoles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=customerly.database.windows.net,1433;Initial Catalog=customerly;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;Authentication=Active Directory Default;");

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
            entity.HasKey(e => e.RoleId);

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

            entity.Property(e => e.UserId).HasMaxLength(50);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.IsApproved).HasDefaultValue(false);
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.Property(e => e.RefreshTokenExpires).HasColumnType("datetime");
        });

        modelBuilder.Entity<Customerly_UserRole>(entity =>
        {
            entity.HasKey(e => e.UserRoleID).IsClustered(false);

            entity.Property(e => e.UserID).HasMaxLength(50);

            entity.HasOne(d => d.Role).WithMany(p => p.Customerly_UserRoles)
                .HasForeignKey(d => d.RoleID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Customerly_UserRoles_Customerly_Roles");

            entity.HasOne(d => d.User).WithMany(p => p.Customerly_UserRoles)
                .HasForeignKey(d => d.UserID)
                .HasConstraintName("FK_UserRole_Users");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
