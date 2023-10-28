﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DoAn_FrameWork.Models
{
    public partial class TechnoShop_DBContext : DbContext
    {
        public TechnoShop_DBContext()
        {
        }

        public TechnoShop_DBContext(DbContextOptions<TechnoShop_DBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<Payment> Payments { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<ProductImage> ProductImages { get; set; } = null!;
        public virtual DbSet<ProductTag> ProductTags { get; set; } = null!;
        public virtual DbSet<Setting> Settings { get; set; } = null!;
        public virtual DbSet<Shipping> Shippings { get; set; } = null!;
        public virtual DbSet<Slider> Sliders { get; set; } = null!;
        public virtual DbSet<Tag> Tags { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-LTA3PDR;Database=TechnoShop_DB;Integrated Security=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("categories");

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.CategoryName)
                    .HasMaxLength(50)
                    .HasColumnName("category_name")
                    .IsFixedLength();
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("customers");

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                entity.Property(e => e.CustomerEmail)
                    .HasMaxLength(50)
                    .HasColumnName("customer_email")
                    .IsFixedLength();

                entity.Property(e => e.CustomerName)
                    .HasMaxLength(50)
                    .HasColumnName("customer_name")
                    .IsFixedLength();

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .HasColumnName("password")
                    .IsFixedLength();

                entity.Property(e => e.Phone)
                    .HasMaxLength(50)
                    .HasColumnName("phone")
                    .IsFixedLength();

                entity.Property(e => e.RePassword)
                    .HasMaxLength(50)
                    .HasColumnName("re_password")
                    .IsFixedLength();

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .HasColumnName("username")
                    .IsFixedLength();
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("orders");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                entity.Property(e => e.OrderStatus).HasColumnName("order_status");

                entity.Property(e => e.OrderTotal).HasColumnName("order_total");

                entity.Property(e => e.PaymentId).HasColumnName("payment_id");

                entity.Property(e => e.ShippingId).HasColumnName("shipping_id");
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.ToTable("payments");

                entity.Property(e => e.PaymentId).HasColumnName("payment_id");

                entity.Property(e => e.PaymentMethod)
                    .HasMaxLength(50)
                    .HasColumnName("payment_method")
                    .IsFixedLength();

                entity.Property(e => e.PaymentStatus)
                    .HasMaxLength(20)
                    .HasColumnName("payment_status")
                    .IsFixedLength();
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("products");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.ProductDesc)
                    .HasMaxLength(50)
                    .HasColumnName("product_desc")
                    .IsFixedLength();

                entity.Property(e => e.ProductImage)
                    .HasMaxLength(50)
                    .HasColumnName("product_image")
                    .IsFixedLength();

                entity.Property(e => e.ProductName)
                    .HasMaxLength(50)
                    .HasColumnName("product_name")
                    .IsFixedLength();

                entity.Property(e => e.ProductPrice).HasColumnName("product_price");
            });

            modelBuilder.Entity<ProductImage>(entity =>
            {
                entity.ToTable("product_image");

                entity.Property(e => e.ProductImageId).HasColumnName("product_image_id");

                entity.Property(e => e.Image)
                    .HasMaxLength(50)
                    .HasColumnName("image")
                    .IsFixedLength();

                entity.Property(e => e.ProductId).HasColumnName("product_id");
            });

            modelBuilder.Entity<ProductTag>(entity =>
            {
                entity.ToTable("product_tag");

                entity.Property(e => e.ProductTagId).HasColumnName("product_tag_id");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.TagId).HasColumnName("tag_id");
            });

            modelBuilder.Entity<Setting>(entity =>
            {
                entity.ToTable("settings");

                entity.Property(e => e.SettingId).HasColumnName("setting_id");

                entity.Property(e => e.ConfigKey)
                    .HasMaxLength(50)
                    .HasColumnName("config_key")
                    .IsFixedLength();

                entity.Property(e => e.ConfigValue)
                    .HasMaxLength(50)
                    .HasColumnName("config_value")
                    .IsFixedLength();
            });

            modelBuilder.Entity<Shipping>(entity =>
            {
                entity.ToTable("shipping");

                entity.Property(e => e.ShippingId).HasColumnName("shipping_id");

                entity.Property(e => e.ShippingAddress)
                    .HasMaxLength(50)
                    .HasColumnName("shipping_address")
                    .IsFixedLength();

                entity.Property(e => e.ShippingEmail)
                    .HasMaxLength(50)
                    .HasColumnName("shipping_email")
                    .IsFixedLength();

                entity.Property(e => e.ShippingName)
                    .HasMaxLength(50)
                    .HasColumnName("shipping_name")
                    .IsFixedLength();

                entity.Property(e => e.ShippingNote)
                    .HasMaxLength(50)
                    .HasColumnName("shipping_note")
                    .IsFixedLength();

                entity.Property(e => e.ShippingPhone)
                    .HasMaxLength(50)
                    .HasColumnName("shipping_phone")
                    .IsFixedLength();
            });

            modelBuilder.Entity<Slider>(entity =>
            {
                entity.ToTable("sliders");

                entity.Property(e => e.SliderId).HasColumnName("slider_id");

                entity.Property(e => e.SliderDesc)
                    .HasMaxLength(50)
                    .HasColumnName("slider_desc")
                    .IsFixedLength();

                entity.Property(e => e.SliderImg)
                    .HasMaxLength(50)
                    .HasColumnName("slider_img")
                    .IsFixedLength();

                entity.Property(e => e.SliderName)
                    .HasMaxLength(50)
                    .HasColumnName("slider_name")
                    .IsFixedLength();
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.ToTable("tags");

                entity.Property(e => e.TagId).HasColumnName("tag_id");

                entity.Property(e => e.TagName)
                    .HasMaxLength(100)
                    .HasColumnName("tag_name")
                    .IsFixedLength();
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.Email)
                    .HasMaxLength(10)
                    .HasColumnName("email")
                    .IsFixedLength();

                entity.Property(e => e.Name)
                    .HasMaxLength(10)
                    .HasColumnName("name")
                    .IsFixedLength();

                entity.Property(e => e.Password)
                    .HasMaxLength(10)
                    .HasColumnName("password")
                    .IsFixedLength();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}