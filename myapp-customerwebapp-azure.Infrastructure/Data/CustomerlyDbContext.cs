using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

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

    public virtual DbSet<CustomerlyCustomer> CustomerlyCustomers { get; set; }

    public virtual DbSet<CustomerlyUser> CustomerlyUsers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer(" Server=tcp:customerly.database.windows.net,1433;Initial Catalog=customerly;Persist Security Info=False;User ID=rahulb9188;Password= Customerly2025##$$;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30; ");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CustomerlyCustomer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__Customer__A4AE64D8C31B31A4");

            entity.ToTable("Customerly_Customers");

            entity.HasIndex(e => e.UserId, "UQ__Customer__1788CC4D291C7F48").IsUnique();

            entity.Property(e => e.CustomerName).HasMaxLength(100);

            entity.HasOne(d => d.User).WithOne(p => p.CustomerlyCustomer)
                .HasForeignKey<CustomerlyCustomer>(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Customerl__UserI__656C112C");
        });

        modelBuilder.Entity<CustomerlyUser>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Customer__1788CC4CF6461D96");

            entity.ToTable("Customerly_Users");

            entity.HasIndex(e => e.Email, "UQ__Customer__A9D10534B6F62E8E").IsUnique();

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.Property(e => e.Role).HasMaxLength(50);
            entity.Property(e => e.UserType)
                .HasMaxLength(20)
                .HasDefaultValue("Customer");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
