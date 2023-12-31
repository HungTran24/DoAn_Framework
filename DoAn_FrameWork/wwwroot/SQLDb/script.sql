USE [master]
GO
/****** Object:  Database [TechnoShop_DB]    Script Date: 12/3/2023 9:14:22 AM ******/
CREATE DATABASE [TechnoShop_DB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'TechnoShop_DB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\TechnoShop_DB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'TechnoShop_DB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\TechnoShop_DB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [TechnoShop_DB] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [TechnoShop_DB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [TechnoShop_DB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [TechnoShop_DB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [TechnoShop_DB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [TechnoShop_DB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [TechnoShop_DB] SET ARITHABORT OFF 
GO
ALTER DATABASE [TechnoShop_DB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [TechnoShop_DB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [TechnoShop_DB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [TechnoShop_DB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [TechnoShop_DB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [TechnoShop_DB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [TechnoShop_DB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [TechnoShop_DB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [TechnoShop_DB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [TechnoShop_DB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [TechnoShop_DB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [TechnoShop_DB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [TechnoShop_DB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [TechnoShop_DB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [TechnoShop_DB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [TechnoShop_DB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [TechnoShop_DB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [TechnoShop_DB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [TechnoShop_DB] SET  MULTI_USER 
GO
ALTER DATABASE [TechnoShop_DB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [TechnoShop_DB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [TechnoShop_DB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [TechnoShop_DB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [TechnoShop_DB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [TechnoShop_DB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'TechnoShop_DB', N'ON'
GO
ALTER DATABASE [TechnoShop_DB] SET QUERY_STORE = ON
GO
ALTER DATABASE [TechnoShop_DB] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [TechnoShop_DB]
GO
/****** Object:  Table [dbo].[categories]    Script Date: 12/3/2023 9:14:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[categories](
	[category_id] [int] IDENTITY(1,1) NOT NULL,
	[category_name] [nvarchar](300) NULL,
 CONSTRAINT [PK_categories] PRIMARY KEY CLUSTERED 
(
	[category_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[customers]    Script Date: 12/3/2023 9:14:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[customers](
	[customer_id] [int] IDENTITY(1,1) NOT NULL,
	[username] [nvarchar](300) NULL,
	[password] [nchar](50) NULL,
	[re_password] [nchar](50) NULL,
	[customer_name] [nvarchar](300) NULL,
	[customer_email] [nvarchar](300) NULL,
	[phone] [nchar](50) NULL,
 CONSTRAINT [PK_customers] PRIMARY KEY CLUSTERED 
(
	[customer_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[group_products]    Script Date: 12/3/2023 9:14:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[group_products](
	[group_product_id] [int] IDENTITY(1,1) NOT NULL,
	[group_product_name] [nvarchar](200) NULL,
 CONSTRAINT [PK_group_products] PRIMARY KEY CLUSTERED 
(
	[group_product_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[order_details]    Script Date: 12/3/2023 9:14:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[order_details](
	[order_details_id] [int] IDENTITY(1,1) NOT NULL,
	[order_id] [int] NULL,
	[product_id] [int] NULL,
	[product_sales_quantity] [int] NULL,
 CONSTRAINT [PK_order_details] PRIMARY KEY CLUSTERED 
(
	[order_details_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[orders]    Script Date: 12/3/2023 9:14:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[orders](
	[order_id] [int] IDENTITY(1,1) NOT NULL,
	[customer_id] [int] NULL,
	[shipping_id] [int] NULL,
	[payment_id] [int] NULL,
	[order_total] [int] NULL,
	[order_status] [int] NULL,
	[created_at] [smalldatetime] NULL,
 CONSTRAINT [PK_orders] PRIMARY KEY CLUSTERED 
(
	[order_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[payments]    Script Date: 12/3/2023 9:14:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[payments](
	[payment_id] [int] IDENTITY(1,1) NOT NULL,
	[payment_method] [nvarchar](300) NULL,
	[payment_status] [nvarchar](300) NULL,
 CONSTRAINT [PK_payments] PRIMARY KEY CLUSTERED 
(
	[payment_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[product_image]    Script Date: 12/3/2023 9:14:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[product_image](
	[product_image_id] [int] IDENTITY(1,1) NOT NULL,
	[image] [varchar](2000) NULL,
	[product_id] [int] NULL,
 CONSTRAINT [PK_product_image] PRIMARY KEY CLUSTERED 
(
	[product_image_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[products]    Script Date: 12/3/2023 9:14:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[products](
	[product_id] [int] IDENTITY(1,1) NOT NULL,
	[product_name] [nvarchar](300) NULL,
	[product_price] [int] NULL,
	[product_image] [varchar](2000) NULL,
	[product_desc] [nvarchar](2000) NULL,
	[discount_percentage] [float] NULL,
	[category_id] [int] NULL,
	[sale_quantity] [int] NULL,
	[stock_quantity] [int] NULL,
	[warranty_time] [int] NULL,
	[group_product_id] [int] NULL,
 CONSTRAINT [PK_products] PRIMARY KEY CLUSTERED 
(
	[product_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[shipping]    Script Date: 12/3/2023 9:14:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[shipping](
	[shipping_id] [int] IDENTITY(1,1) NOT NULL,
	[shipping_name] [nvarchar](300) NULL,
	[shipping_address] [nvarchar](300) NULL,
	[shipping_phone] [nvarchar](300) NULL,
	[shipping_email] [nvarchar](300) NULL,
	[shipping_note] [nvarchar](300) NULL,
 CONSTRAINT [PK_shipping] PRIMARY KEY CLUSTERED 
(
	[shipping_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[products] ADD  DEFAULT ((0)) FOR [discount_percentage]
GO
ALTER TABLE [dbo].[products] ADD  DEFAULT ((1)) FOR [category_id]
GO
ALTER TABLE [dbo].[products] ADD  DEFAULT ((0)) FOR [sale_quantity]
GO
ALTER TABLE [dbo].[products] ADD  DEFAULT ((1)) FOR [stock_quantity]
GO
ALTER TABLE [dbo].[order_details]  WITH CHECK ADD  CONSTRAINT [FK_order_details_orders] FOREIGN KEY([order_id])
REFERENCES [dbo].[orders] ([order_id])
GO
ALTER TABLE [dbo].[order_details] CHECK CONSTRAINT [FK_order_details_orders]
GO
ALTER TABLE [dbo].[order_details]  WITH CHECK ADD  CONSTRAINT [FK_order_details_products] FOREIGN KEY([product_id])
REFERENCES [dbo].[products] ([product_id])
GO
ALTER TABLE [dbo].[order_details] CHECK CONSTRAINT [FK_order_details_products]
GO
ALTER TABLE [dbo].[orders]  WITH CHECK ADD  CONSTRAINT [FK_orders_customers] FOREIGN KEY([customer_id])
REFERENCES [dbo].[customers] ([customer_id])
GO
ALTER TABLE [dbo].[orders] CHECK CONSTRAINT [FK_orders_customers]
GO
ALTER TABLE [dbo].[orders]  WITH CHECK ADD  CONSTRAINT [FK_orders_payments] FOREIGN KEY([payment_id])
REFERENCES [dbo].[payments] ([payment_id])
GO
ALTER TABLE [dbo].[orders] CHECK CONSTRAINT [FK_orders_payments]
GO
ALTER TABLE [dbo].[orders]  WITH CHECK ADD  CONSTRAINT [FK_orders_shipping] FOREIGN KEY([shipping_id])
REFERENCES [dbo].[shipping] ([shipping_id])
GO
ALTER TABLE [dbo].[orders] CHECK CONSTRAINT [FK_orders_shipping]
GO
ALTER TABLE [dbo].[product_image]  WITH CHECK ADD  CONSTRAINT [FK_product_image_products] FOREIGN KEY([product_id])
REFERENCES [dbo].[products] ([product_id])
GO
ALTER TABLE [dbo].[product_image] CHECK CONSTRAINT [FK_product_image_products]
GO
ALTER TABLE [dbo].[products]  WITH CHECK ADD  CONSTRAINT [FK_products_categories] FOREIGN KEY([category_id])
REFERENCES [dbo].[categories] ([category_id])
GO
ALTER TABLE [dbo].[products] CHECK CONSTRAINT [FK_products_categories]
GO
ALTER TABLE [dbo].[products]  WITH CHECK ADD  CONSTRAINT [FK_products_group_products] FOREIGN KEY([group_product_id])
REFERENCES [dbo].[group_products] ([group_product_id])
GO
ALTER TABLE [dbo].[products] CHECK CONSTRAINT [FK_products_group_products]
GO
USE [master]
GO
ALTER DATABASE [TechnoShop_DB] SET  READ_WRITE 
GO
