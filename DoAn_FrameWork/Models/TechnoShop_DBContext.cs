using System;
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
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-LTA3PDR;Initial Catalog=TechnoShop_DB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
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

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_orders_customers");

                entity.HasOne(d => d.Shipping)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.ShippingId)
                    .HasConstraintName("FK_orders_shipping");
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

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_products_categories");
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

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductImages)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_product_image_products");
            });

            modelBuilder.Entity<ProductTag>(entity =>
            {
                entity.ToTable("product_tag");

                entity.Property(e => e.ProductTagId).HasColumnName("product_tag_id");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.TagId).HasColumnName("tag_id");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductTags)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_product_tag_products");
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

        //internal IEnumerable<Category> Categories()
        //{
        //    throw new NotImplementedException();
        //}

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
