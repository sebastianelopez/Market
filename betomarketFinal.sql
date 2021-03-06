USE [master]
GO
/****** Object:  Database [betomarket_tp4]    Script Date: 8/2/2022 19:47:13 ******/
CREATE DATABASE [betomarket_tp4]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'betomarket_tp4', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\betomarket_tp4.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'betomarket_tp4_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\betomarket_tp4_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [betomarket_tp4] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [betomarket_tp4].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [betomarket_tp4] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [betomarket_tp4] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [betomarket_tp4] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [betomarket_tp4] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [betomarket_tp4] SET ARITHABORT OFF 
GO
ALTER DATABASE [betomarket_tp4] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [betomarket_tp4] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [betomarket_tp4] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [betomarket_tp4] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [betomarket_tp4] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [betomarket_tp4] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [betomarket_tp4] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [betomarket_tp4] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [betomarket_tp4] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [betomarket_tp4] SET  DISABLE_BROKER 
GO
ALTER DATABASE [betomarket_tp4] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [betomarket_tp4] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [betomarket_tp4] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [betomarket_tp4] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [betomarket_tp4] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [betomarket_tp4] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [betomarket_tp4] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [betomarket_tp4] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [betomarket_tp4] SET  MULTI_USER 
GO
ALTER DATABASE [betomarket_tp4] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [betomarket_tp4] SET DB_CHAINING OFF 
GO
ALTER DATABASE [betomarket_tp4] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [betomarket_tp4] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [betomarket_tp4] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [betomarket_tp4] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [betomarket_tp4] SET QUERY_STORE = OFF
GO
USE [betomarket_tp4]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 8/2/2022 19:47:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cart]    Script Date: 8/2/2022 19:47:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cart](
	[cartId] [int] IDENTITY(1,1) NOT NULL,
	[userId] [int] NOT NULL,
 CONSTRAINT [PK_Cart] PRIMARY KEY CLUSTERED 
(
	[cartId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CartProduct]    Script Date: 8/2/2022 19:47:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CartProduct](
	[cartId] [int] NOT NULL,
	[productId] [int] NOT NULL,
	[id] [int] NOT NULL,
	[ammount] [int] NOT NULL,
 CONSTRAINT [PK_CartProduct] PRIMARY KEY CLUSTERED 
(
	[cartId] ASC,
	[productId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CartPurchase]    Script Date: 8/2/2022 19:47:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CartPurchase](
	[purchaseId] [int] NOT NULL,
	[productId] [int] NOT NULL,
	[id] [int] NOT NULL,
 CONSTRAINT [PK_CartPurchase] PRIMARY KEY CLUSTERED 
(
	[purchaseId] ASC,
	[productId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 8/2/2022 19:47:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[categoryId] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[categoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Coupons]    Script Date: 8/2/2022 19:47:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Coupons](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[code] [nvarchar](max) NOT NULL,
	[percentage] [int] NOT NULL,
	[CartPurchaseproductId] [int] NULL,
	[CartPurchasepurchaseId] [int] NULL,
 CONSTRAINT [PK_Coupons] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Logs]    Script Date: 8/2/2022 19:47:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Logs](
	[logId] [int] IDENTITY(1,1) NOT NULL,
	[eventType] [int] NOT NULL,
	[userId] [int] NULL,
	[createdAt] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Logs] PRIMARY KEY CLUSTERED 
(
	[logId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 8/2/2022 19:47:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[productId] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](max) NOT NULL,
	[price] [float] NOT NULL,
	[description] [nvarchar](max) NOT NULL,
	[ammount] [int] NOT NULL,
	[categoryId] [int] NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[productId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PurchaseCoupon]    Script Date: 8/2/2022 19:47:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PurchaseCoupon](
	[purchaseId] [int] NOT NULL,
	[couponId] [int] NOT NULL,
	[id] [int] NOT NULL,
 CONSTRAINT [PK_PurchaseCoupon] PRIMARY KEY CLUSTERED 
(
	[purchaseId] ASC,
	[couponId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Purchases]    Script Date: 8/2/2022 19:47:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Purchases](
	[purchaseId] [int] IDENTITY(1,1) NOT NULL,
	[total] [float] NOT NULL,
	[buyeruserId] [int] NULL,
 CONSTRAINT [PK_Purchases] PRIMARY KEY CLUSTERED 
(
	[purchaseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 8/2/2022 19:47:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[userId] [int] IDENTITY(1,1) NOT NULL,
	[dni] [bigint] NOT NULL,
	[name] [nvarchar](max) NOT NULL,
	[lastName] [nvarchar](max) NULL,
	[email] [nvarchar](max) NOT NULL,
	[CUITCUIL] [bigint] NOT NULL,
	[password] [nvarchar](max) NOT NULL,
	[userType] [nvarchar](max) NOT NULL,
	[attemps] [int] NOT NULL,
	[locked] [bit] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[userId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Index [IX_Cart_userId]    Script Date: 8/2/2022 19:47:13 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Cart_userId] ON [dbo].[Cart]
(
	[userId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_CartProduct_productId]    Script Date: 8/2/2022 19:47:13 ******/
CREATE NONCLUSTERED INDEX [IX_CartProduct_productId] ON [dbo].[CartProduct]
(
	[productId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_CartPurchase_productId]    Script Date: 8/2/2022 19:47:13 ******/
CREATE NONCLUSTERED INDEX [IX_CartPurchase_productId] ON [dbo].[CartPurchase]
(
	[productId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Coupons_CartPurchasepurchaseId_CartPurchaseproductId]    Script Date: 8/2/2022 19:47:13 ******/
CREATE NONCLUSTERED INDEX [IX_Coupons_CartPurchasepurchaseId_CartPurchaseproductId] ON [dbo].[Coupons]
(
	[CartPurchasepurchaseId] ASC,
	[CartPurchaseproductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Logs_userId]    Script Date: 8/2/2022 19:47:13 ******/
CREATE NONCLUSTERED INDEX [IX_Logs_userId] ON [dbo].[Logs]
(
	[userId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Products_categoryId]    Script Date: 8/2/2022 19:47:13 ******/
CREATE NONCLUSTERED INDEX [IX_Products_categoryId] ON [dbo].[Products]
(
	[categoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_PurchaseCoupon_couponId]    Script Date: 8/2/2022 19:47:13 ******/
CREATE NONCLUSTERED INDEX [IX_PurchaseCoupon_couponId] ON [dbo].[PurchaseCoupon]
(
	[couponId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Purchases_buyeruserId]    Script Date: 8/2/2022 19:47:13 ******/
CREATE NONCLUSTERED INDEX [IX_Purchases_buyeruserId] ON [dbo].[Purchases]
(
	[buyeruserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT (CONVERT([bit],(0))) FOR [locked]
GO
ALTER TABLE [dbo].[Cart]  WITH CHECK ADD  CONSTRAINT [FK_Cart_Users_userId] FOREIGN KEY([userId])
REFERENCES [dbo].[Users] ([userId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Cart] CHECK CONSTRAINT [FK_Cart_Users_userId]
GO
ALTER TABLE [dbo].[CartProduct]  WITH CHECK ADD  CONSTRAINT [FK_CartProduct_Cart_cartId] FOREIGN KEY([cartId])
REFERENCES [dbo].[Cart] ([cartId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CartProduct] CHECK CONSTRAINT [FK_CartProduct_Cart_cartId]
GO
ALTER TABLE [dbo].[CartProduct]  WITH CHECK ADD  CONSTRAINT [FK_CartProduct_Products_productId] FOREIGN KEY([productId])
REFERENCES [dbo].[Products] ([productId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CartProduct] CHECK CONSTRAINT [FK_CartProduct_Products_productId]
GO
ALTER TABLE [dbo].[CartPurchase]  WITH CHECK ADD  CONSTRAINT [FK_CartPurchase_Products_productId] FOREIGN KEY([productId])
REFERENCES [dbo].[Products] ([productId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CartPurchase] CHECK CONSTRAINT [FK_CartPurchase_Products_productId]
GO
ALTER TABLE [dbo].[CartPurchase]  WITH CHECK ADD  CONSTRAINT [FK_CartPurchase_Purchases_purchaseId] FOREIGN KEY([purchaseId])
REFERENCES [dbo].[Purchases] ([purchaseId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CartPurchase] CHECK CONSTRAINT [FK_CartPurchase_Purchases_purchaseId]
GO
ALTER TABLE [dbo].[Coupons]  WITH CHECK ADD  CONSTRAINT [FK_Coupons_CartPurchase_CartPurchasepurchaseId_CartPurchaseproductId] FOREIGN KEY([CartPurchasepurchaseId], [CartPurchaseproductId])
REFERENCES [dbo].[CartPurchase] ([purchaseId], [productId])
GO
ALTER TABLE [dbo].[Coupons] CHECK CONSTRAINT [FK_Coupons_CartPurchase_CartPurchasepurchaseId_CartPurchaseproductId]
GO
ALTER TABLE [dbo].[Logs]  WITH CHECK ADD  CONSTRAINT [FK_Logs_Users_userId] FOREIGN KEY([userId])
REFERENCES [dbo].[Users] ([userId])
GO
ALTER TABLE [dbo].[Logs] CHECK CONSTRAINT [FK_Logs_Users_userId]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Products_Categories_categoryId] FOREIGN KEY([categoryId])
REFERENCES [dbo].[Categories] ([categoryId])
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_Categories_categoryId]
GO
ALTER TABLE [dbo].[PurchaseCoupon]  WITH CHECK ADD  CONSTRAINT [FK_PurchaseCoupon_Coupons_couponId] FOREIGN KEY([couponId])
REFERENCES [dbo].[Coupons] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PurchaseCoupon] CHECK CONSTRAINT [FK_PurchaseCoupon_Coupons_couponId]
GO
ALTER TABLE [dbo].[PurchaseCoupon]  WITH CHECK ADD  CONSTRAINT [FK_PurchaseCoupon_Purchases_purchaseId] FOREIGN KEY([purchaseId])
REFERENCES [dbo].[Purchases] ([purchaseId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PurchaseCoupon] CHECK CONSTRAINT [FK_PurchaseCoupon_Purchases_purchaseId]
GO
ALTER TABLE [dbo].[Purchases]  WITH CHECK ADD  CONSTRAINT [FK_Purchases_Users_buyeruserId] FOREIGN KEY([buyeruserId])
REFERENCES [dbo].[Users] ([userId])
GO
ALTER TABLE [dbo].[Purchases] CHECK CONSTRAINT [FK_Purchases_Users_buyeruserId]
GO
USE [master]
GO
ALTER DATABASE [betomarket_tp4] SET  READ_WRITE 
GO
