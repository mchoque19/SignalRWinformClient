using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BackOfficeMonitorCocina.Models;

public partial class MadisaKitchenContext : DbContext
{
    public MadisaKitchenContext()
    {
    }

    public MadisaKitchenContext(DbContextOptions<MadisaKitchenContext> options)
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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=.;Database=MadisaKitchen;User=root;Password=mymadisa;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Article>(entity =>
        {
            entity.ToTable("Article");

            entity.HasIndex(e => e.DepartmentId, "IX_Article_DepartmentId");

            entity.HasOne(d => d.Department).WithMany(p => p.Articles).HasForeignKey(d => d.DepartmentId);

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
            entity.ToTable("Department");

            entity.HasIndex(e => e.FamilyId, "IX_Department_FamilyId");

            entity.HasOne(d => d.Family).WithMany(p => p.Departments).HasForeignKey(d => d.FamilyId);

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
            entity.ToTable("Family");

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
            entity.ToTable("Monitor");

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

        modelBuilder.Entity<Order>(entity =>
        {
            entity.ToTable("Order");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(e => new { e.OrderId, e.OrderLineNo });

            entity.ToTable("OrderItem");

            entity.HasIndex(e => e.ArticleId, "IX_OrderItem_ArticleId");

            entity.HasIndex(e => e.PrintOrderGroupId, "IX_OrderItem_PrintOrderGroupId");

            entity.HasIndex(e => e.StateId, "IX_OrderItem_StateId");

            entity.HasOne(d => d.Article).WithMany(p => p.OrderItems).HasForeignKey(d => d.ArticleId);

            entity.HasOne(d => d.PrintOrderGroup).WithMany(p => p.OrderItems).HasForeignKey(d => d.PrintOrderGroupId);

            entity.HasOne(d => d.State).WithMany(p => p.OrderItems).HasForeignKey(d => d.StateId);
        });

        modelBuilder.Entity<PrintOrder>(entity =>
        {
            entity.ToTable("PrintOrder");
        });

        modelBuilder.Entity<State>(entity =>
        {
            entity.ToTable("State");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
