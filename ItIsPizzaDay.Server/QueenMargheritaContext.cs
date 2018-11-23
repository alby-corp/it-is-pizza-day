﻿using Microsoft.EntityFrameworkCore;

namespace ItIsPizzaDay.Server
{
    using Shared.Models;

    public partial class QueenMargheritaContext : DbContext
    {
        public QueenMargheritaContext()
        {
        }

        public QueenMargheritaContext(DbContextOptions options)
            : base(options)
        {
        }

        public virtual DbSet<Food> Food { get; set; }
        public virtual DbSet<FoodOrder> FoodOrder { get; set; }
        public virtual DbSet<FoodOrderIngredient> FoodOrderIngredient { get; set; }
        public virtual DbSet<Ingredient> Ingredient { get; set; }
        public virtual DbSet<Muppet> Muppet { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<Type> Type { get; set; }

        // Unable to generate entity type for table 'public.food_ingredient'. Please see the warning messages.

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                
                optionsBuilder.UseNpgsql("Host=localhost;Port=5555;Database=QueenMargherita;Username=SamuraiTeam;Password=SamuraiTeam");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("uuid-ossp");

            modelBuilder.Entity<Food>(entity =>
            {
                entity.ToTable("food");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name");

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("numeric(4,2)");

                entity.Property(e => e.Type).HasColumnName("type");

                entity.Property(e => e.Visible).HasColumnName("visible");

                entity.HasOne(d => d.TypeNavigation)
                    .WithMany(p => p.Food)
                    .HasForeignKey(d => d.Type)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("food_type_fkey");
            });

            modelBuilder.Entity<FoodOrder>(entity =>
            {
                entity.ToTable("food_order");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Food).HasColumnName("food");

                entity.Property(e => e.Order).HasColumnName("order");

                entity.HasOne(d => d.FoodNavigation)
                    .WithMany(p => p.FoodOrder)
                    .HasForeignKey(d => d.Food)
                    .HasConstraintName("food_order_food_fkey");

                entity.HasOne(d => d.OrderNavigation)
                    .WithMany(p => p.FoodOrder)
                    .HasForeignKey(d => d.Order)
                    .HasConstraintName("food_order_order_fkey");
            });

            modelBuilder.Entity<FoodOrderIngredient>(entity =>
            {
                entity.ToTable("food_order_ingredient");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.FoodOrder).HasColumnName("food_order");

                entity.Property(e => e.Ingredient).HasColumnName("ingredient");

                entity.Property(e => e.Isremoval).HasColumnName("isremoval");

                entity.HasOne(d => d.FoodOrderNavigation)
                    .WithMany(p => p.FoodOrderIngredient)
                    .HasForeignKey(d => d.FoodOrder)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("food_order_ingredient_food_order_fkey");

                entity.HasOne(d => d.IngredientNavigation)
                    .WithMany(p => p.FoodOrderIngredient)
                    .HasForeignKey(d => d.Ingredient)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("food_order_ingredient_ingredient_fkey");
            });

            modelBuilder.Entity<Ingredient>(entity =>
            {
                entity.ToTable("ingredient");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name");

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("numeric(4,2)");
            });

            modelBuilder.Entity<Muppet>(entity =>
            {
                entity.ToTable("muppet");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.RealName).HasColumnName("real_name");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("order");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Date).HasColumnName("date");

                entity.Property(e => e.Muppet).HasColumnName("muppet");

                entity.HasOne(d => d.MuppetNavigation)
                    .WithMany(p => p.Order)
                    .HasForeignKey(d => d.Muppet)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("order_muppet_fkey");
            });

            modelBuilder.Entity<Type>(entity =>
            {
                entity.ToTable("type");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description");
            });
        }
    }
}
