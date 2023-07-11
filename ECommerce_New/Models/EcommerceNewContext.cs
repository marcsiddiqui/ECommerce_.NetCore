using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ECommerce_New.Models;

public partial class EcommerceNewContext : DbContext
{
    public EcommerceNewContext()
    {
    }

    public EcommerceNewContext(DbContextOptions<EcommerceNewContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Customer__3214EC075FBEBD89");

            entity.ToTable("Customer");

            entity.Property(e => e.Cnic)
                .HasMaxLength(256)
                .HasColumnName("CNIC");
            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.FirstName).HasMaxLength(256);
            entity.Property(e => e.LastName).HasMaxLength(256);
            entity.Property(e => e.Password).HasMaxLength(256);
            entity.Property(e => e.PhoneNumber).HasMaxLength(256);
            entity.Property(e => e.Username).HasMaxLength(256);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Role__3214EC07849F4B56");

            entity.ToTable("Role");

            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.Name).HasMaxLength(200);
        });
        modelBuilder.HasSequence("sequence_1")
            .HasMin(0L)
            .IsCyclic();

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
