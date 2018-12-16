﻿// <auto-generated />
using System;
using ItIsPizzaDay.Server;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ItIsPizzaDay.Server.Migrations
{
    [DbContext(typeof(QueenMargheritaContext))]
    [Migration("20181213150159_muppet.isAdmin")]
    partial class muppetisAdmin
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("ItIsPizzaDay.Shared.Models.Food", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name");

                    b.Property<decimal>("Price")
                        .HasColumnName("price")
                        .HasColumnType("numeric(4,2)");

                    b.Property<Guid>("Type")
                        .HasColumnName("type");

                    b.Property<bool>("Visible")
                        .HasColumnName("visible");

                    b.HasKey("Id");

                    b.HasIndex("Type");

                    b.ToTable("food");
                });

            modelBuilder.Entity("ItIsPizzaDay.Shared.Models.FoodIngredient", b =>
                {
                    b.Property<Guid>("Food")
                        .HasColumnName("food");

                    b.Property<Guid>("Ingredient")
                        .HasColumnName("ingredient");

                    b.HasKey("Food", "Ingredient");

                    b.HasIndex("Ingredient");

                    b.ToTable("food_ingredient");
                });

            modelBuilder.Entity("ItIsPizzaDay.Shared.Models.FoodOrder", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<Guid?>("Food")
                        .HasColumnName("food");

                    b.Property<Guid?>("Order")
                        .HasColumnName("order");

                    b.HasKey("Id");

                    b.HasIndex("Food");

                    b.HasIndex("Order");

                    b.ToTable("food_order");
                });

            modelBuilder.Entity("ItIsPizzaDay.Shared.Models.FoodOrderIngredient", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<Guid>("FoodOrder")
                        .HasColumnName("food_order");

                    b.Property<Guid>("Ingredient")
                        .HasColumnName("ingredient");

                    b.Property<bool>("Isremoval")
                        .HasColumnName("isremoval");

                    b.HasKey("Id");

                    b.HasIndex("FoodOrder");

                    b.HasIndex("Ingredient");

                    b.ToTable("food_order_ingredient");
                });

            modelBuilder.Entity("ItIsPizzaDay.Shared.Models.Ingredient", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name");

                    b.Property<decimal?>("Price")
                        .HasColumnName("price")
                        .HasColumnType("numeric(4,2)");

                    b.HasKey("Id");

                    b.ToTable("ingredient");
                });

            modelBuilder.Entity("ItIsPizzaDay.Shared.Models.Muppet", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnName("id");

                    b.Property<bool>("IsAdmin")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<string>("RealName")
                        .HasColumnName("real_name");

                    b.HasKey("Id");

                    b.ToTable("muppet");
                });

            modelBuilder.Entity("ItIsPizzaDay.Shared.Models.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<DateTime?>("Date")
                        .HasColumnName("date");

                    b.Property<Guid>("Muppet")
                        .HasColumnName("muppet");

                    b.HasKey("Id");

                    b.HasIndex("Muppet");

                    b.ToTable("order");
                });

            modelBuilder.Entity("ItIsPizzaDay.Shared.Models.Type", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnName("id");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnName("description");

                    b.HasKey("Id");

                    b.ToTable("type");
                });

            modelBuilder.Entity("ItIsPizzaDay.Shared.Models.Food", b =>
                {
                    b.HasOne("ItIsPizzaDay.Shared.Models.Type", "TypeNavigation")
                        .WithMany("Food")
                        .HasForeignKey("Type")
                        .HasConstraintName("food_type_fkey")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("ItIsPizzaDay.Shared.Models.FoodIngredient", b =>
                {
                    b.HasOne("ItIsPizzaDay.Shared.Models.Food")
                        .WithMany("FoodIngredient")
                        .HasForeignKey("Food")
                        .HasConstraintName("food_ingredient_food_id_fk")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ItIsPizzaDay.Shared.Models.Ingredient", "IngredientNavigation")
                        .WithMany()
                        .HasForeignKey("Ingredient")
                        .HasConstraintName("food_ingredient_ingredient_id_fk")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ItIsPizzaDay.Shared.Models.FoodOrder", b =>
                {
                    b.HasOne("ItIsPizzaDay.Shared.Models.Food", "FoodNavigation")
                        .WithMany("FoodOrder")
                        .HasForeignKey("Food")
                        .HasConstraintName("food_order_food_fkey");

                    b.HasOne("ItIsPizzaDay.Shared.Models.Order", "OrderNavigation")
                        .WithMany("FoodOrder")
                        .HasForeignKey("Order")
                        .HasConstraintName("food_order_order_fkey")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ItIsPizzaDay.Shared.Models.FoodOrderIngredient", b =>
                {
                    b.HasOne("ItIsPizzaDay.Shared.Models.FoodOrder", "FoodOrderNavigation")
                        .WithMany("FoodOrderIngredient")
                        .HasForeignKey("FoodOrder")
                        .HasConstraintName("food_order_ingredient_food_order_fkey")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ItIsPizzaDay.Shared.Models.Ingredient", "IngredientNavigation")
                        .WithMany("FoodOrderIngredient")
                        .HasForeignKey("Ingredient")
                        .HasConstraintName("food_order_ingredient_ingredient_fkey")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ItIsPizzaDay.Shared.Models.Order", b =>
                {
                    b.HasOne("ItIsPizzaDay.Shared.Models.Muppet", "MuppetNavigation")
                        .WithMany("Order")
                        .HasForeignKey("Muppet")
                        .HasConstraintName("order_muppet_fkey")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
