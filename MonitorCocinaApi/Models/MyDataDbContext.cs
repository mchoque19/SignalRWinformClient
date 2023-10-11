using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MonitorCocinaApi.Models;

public partial class MyDataDbContext : DbContext
{
    public MyDataDbContext()
    {
    }

    public MyDataDbContext(DbContextOptions<MyDataDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Article> Articles { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Family> Families { get; set; }

    public virtual DbSet<Monitor> Monitors { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }

    public virtual DbSet<PrintOrder> PrintOrders { get; set; }

    public virtual DbSet<State> States { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;DataBase=MadisaKitchen;Trusted_Connection=True;Encrypt=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Article>(entity =>
        {
            entity.HasMany(d => d.Monitors).WithMany(p => p.Articles)
                .UsingEntity<Dictionary<string, object>>(
                    "ArticleMonitor",
                    r => r.HasOne<Monitor>().WithMany().HasForeignKey("MonitorsId"),
                    l => l.HasOne<Article>().WithMany().HasForeignKey("ArticlesId"),
                    j =>
                    {
                        j.HasKey("ArticlesId", "MonitorsId");
                        j.ToTable("ArticleMonitor");
                        j.HasIndex(new[] { "MonitorsId" }, "IX_ArticleMonitor_MonitorsId");
                    });
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasMany(d => d.Monitors).WithMany(p => p.Departments)
                .UsingEntity<Dictionary<string, object>>(
                    "DepartmentMonitor",
                    r => r.HasOne<Monitor>().WithMany().HasForeignKey("MonitorsId"),
                    l => l.HasOne<Department>().WithMany().HasForeignKey("DepartmentsId"),
                    j =>
                    {
                        j.HasKey("DepartmentsId", "MonitorsId");
                        j.ToTable("DepartmentMonitor");
                        j.HasIndex(new[] { "MonitorsId" }, "IX_DepartmentMonitor_MonitorsId");
                    });
        });

        modelBuilder.Entity<Family>(entity =>
        {
            entity.HasMany(d => d.Monitors).WithMany(p => p.Families)
                .UsingEntity<Dictionary<string, object>>(
                    "FamilyMonitor",
                    r => r.HasOne<Monitor>().WithMany().HasForeignKey("MonitorsId"),
                    l => l.HasOne<Family>().WithMany().HasForeignKey("FamiliesId"),
                    j =>
                    {
                        j.HasKey("FamiliesId", "MonitorsId");
                        j.ToTable("FamilyMonitor");
                        j.HasIndex(new[] { "MonitorsId" }, "IX_FamilyMonitor_MonitorsId");
                    });
        });

        modelBuilder.Entity<Monitor>(entity =>
        {
            entity.HasMany(d => d.States).WithMany(p => p.Monitors)
                .UsingEntity<Dictionary<string, object>>(
                    "MonitorState",
                    r => r.HasOne<State>().WithMany().HasForeignKey("StatesId"),
                    l => l.HasOne<Monitor>().WithMany().HasForeignKey("MonitorsId"),
                    j =>
                    {
                        j.HasKey("MonitorsId", "StatesId");
                        j.ToTable("MonitorState");
                        j.HasIndex(new[] { "StatesId" }, "IX_MonitorState_StatesId");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
