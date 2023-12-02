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
        public virtual DbSet<Option> Options { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<OrderDetail> OrderDetails { get; set; } = null!;
        public virtual DbSet<Payment> Payments { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<ProductDetail> ProductDetails { get; set; } = null!;
        public virtual DbSet<ProductImage> ProductImages { get; set; } = null!;
        public virtual DbSet<Shipping> Shippings { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=Tri;Initial Catalog=TechnoShop_DB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("categories");

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.CategoryName)
                    .HasMaxLength(300)
                    .HasColumnName("category_name")
                    .IsFixedLength();
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("customers");

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                entity.Property(e => e.CustomerEmail)
                    .HasMaxLength(300)
                    .HasColumnName("customer_email")
                    .IsFixedLength();

                entity.Property(e => e.CustomerName)
                    .HasMaxLength(300)
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
                    .HasMaxLength(300)
                    .HasColumnName("username")
                    .IsFixedLength();
            });

            modelBuilder.Entity<Option>(entity =>
            {
                entity.ToTable("options");

                entity.Property(e => e.OptionId).HasColumnName("option_id");

                entity.Property(e => e.OptionImage)
                    .HasMaxLength(200)
                    .HasColumnName("option_image");

                entity.Property(e => e.OptionName)
                    .HasMaxLength(200)
                    .HasColumnName("option_name");

                entity.Property(e => e.OptionPrice).HasColumnName("option_price");

                entity.Property(e => e.OptionSaleQuantity)
                    .HasColumnName("option_sale_quantity")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.OptionStockQuantity)
                    .HasColumnName("option_stock_quantity")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Options)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_options_products");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("orders");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                entity.Property(e => e.OrderStatus).HasColumnName("order_status");

                entity.Property(e => e.OrderTotal).HasColumnName("order_total");

                entity.Property(e => e.PaymentId).HasColumnName("payment_id");

                entity.Property(e => e.ShippingId).HasColumnName("shipping_id");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_orders_customers");

                entity.HasOne(d => d.Payment)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.PaymentId)
                    .HasConstraintName("FK_orders_payments");

                entity.HasOne(d => d.Shipping)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.ShippingId)
                    .HasConstraintName("FK_orders_shipping");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.HasKey(e => e.OrderDetailsId);

                entity.ToTable("order_details");

                entity.Property(e => e.OrderDetailsId).HasColumnName("order_details_id");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.ProductSalesQuantity).HasColumnName("product_sales_quantity");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_order_details_orders");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_order_details_products");
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

                entity.Property(e => e.CategoryId)
                    .HasColumnName("category_id")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.DiscountPercentage)
                    .HasColumnName("discount_percentage")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ProductDesc)
                    .HasMaxLength(1000)
                    .HasColumnName("product_desc")
                    .IsFixedLength();

                entity.Property(e => e.ProductImage)
                    .HasMaxLength(50)
                    .HasColumnName("product_image")
                    .IsFixedLength();

                entity.Property(e => e.ProductName)
                    .HasMaxLength(300)
                    .HasColumnName("product_name")
                    .IsFixedLength();

                entity.Property(e => e.ProductPrice).HasColumnName("product_price");

                entity.Property(e => e.SaleQuantity)
                    .HasColumnName("sale_quantity")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.StockQuantity)
                    .HasColumnName("stock_quantity")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.WarrantyTime).HasColumnName("warranty_time");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_products_categories");
            });

            modelBuilder.Entity<ProductDetail>(entity =>
            {
                entity.ToTable("product_details");

                entity.Property(e => e.ProductDetailId).HasColumnName("product_detail_id");

                entity.Property(e => e.OptionId).HasColumnName("option_id");

                entity.Property(e => e.ProductDetailDesc)
                    .HasMaxLength(200)
                    .HasColumnName("product_detail_desc");

                entity.Property(e => e.ProductDetailName)
                    .HasMaxLength(100)
                    .HasColumnName("product_detail_name");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.HasOne(d => d.Option)
                    .WithMany(p => p.ProductDetails)
                    .HasForeignKey(d => d.OptionId)
                    .HasConstraintName("FK_product_details_options");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductDetails)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_product_details_products");
            });

            modelBuilder.Entity<ProductImage>(entity =>
            {
                entity.ToTable("product_image");

                entity.Property(e => e.ProductImageId).HasColumnName("product_image_id");

                entity.Property(e => e.Image)
                    .HasMaxLength(50)
                    .HasColumnName("image")
                    .IsFixedLength();

                entity.Property(e => e.OptionId).HasColumnName("option_id");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.HasOne(d => d.Option)
                    .WithMany(p => p.ProductImages)
                    .HasForeignKey(d => d.OptionId)
                    .HasConstraintName("FK_product_image_options1");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductImages)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_product_image_products");
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

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
