USE [master]
GO
/****** Object:  Database [Store]    Script Date: 10/31/2023 11:52:52 AM ******/
CREATE DATABASE [Store]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Store', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\Store.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Store_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\Store_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [Store] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Store].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Store] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Store] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Store] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Store] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Store] SET ARITHABORT OFF 
GO
ALTER DATABASE [Store] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Store] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Store] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Store] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Store] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Store] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Store] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Store] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Store] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Store] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Store] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Store] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Store] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Store] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Store] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Store] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Store] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Store] SET RECOVERY FULL 
GO
ALTER DATABASE [Store] SET  MULTI_USER 
GO
ALTER DATABASE [Store] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Store] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Store] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Store] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Store] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Store] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [Store] SET QUERY_STORE = OFF
GO
USE [Store]
GO
/****** Object:  UserDefinedFunction [dbo].[GetYears]    Script Date: 10/31/2023 11:52:53 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create function [dbo].[GetYears] (@Time DateTime)
returns int
As
Begin
	Declare @year int;
	if Year(GETDATE()) > Year(@Time)
		Begin
		Set @year = Year(GETDATE()) - Year(@Time);
		Return @year;
		End
	Return 0;
End;
GO
/****** Object:  Table [dbo].[Addresses]    Script Date: 10/31/2023 11:52:53 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Addresses](
	[AddressID] [int] IDENTITY(1,1) NOT NULL,
	[City] [varchar](30) NOT NULL,
	[PostCode] [int] NOT NULL,
	[CompleteAddress] [varchar](max) NULL,
 CONSTRAINT [PK_Addresses] PRIMARY KEY CLUSTERED 
(
	[AddressID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CustomerAddresses]    Script Date: 10/31/2023 11:52:53 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerAddresses](
	[CustomerID] [int] NOT NULL,
	[AddressID] [int] NOT NULL,
	[DateFrom] [datetime] NOT NULL,
	[DateTo] [datetime] NULL,
 CONSTRAINT [Composite_Key] PRIMARY KEY CLUSTERED 
(
	[AddressID] ASC,
	[CustomerID] ASC,
	[DateFrom] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CustomerOrders]    Script Date: 10/31/2023 11:52:53 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerOrders](
	[OrderID] [int] IDENTITY(1,1) NOT NULL,
	[CustomerID] [int] NOT NULL,
	[DateOrderPlaced] [datetime] NOT NULL,
	[OrderPrice] [int] NOT NULL,
	[DateOrderPaid] [datetime] NULL,
	[PaymentMethodID] [int] NOT NULL,
	[OrderStatusCode] [int] NOT NULL,
 CONSTRAINT [PK_CustomerOrders] PRIMARY KEY CLUSTERED 
(
	[OrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CustomerOrdersDelivery]    Script Date: 10/31/2023 11:52:53 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerOrdersDelivery](
	[OrderID] [int] NOT NULL,
	[DateReported] [datetime] NOT NULL,
	[DeliveryStatusCode] [int] NOT NULL,
 CONSTRAINT [PK_CustomerOrdersDelivery] PRIMARY KEY CLUSTERED 
(
	[DateReported] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CustomerOrdersProducts]    Script Date: 10/31/2023 11:52:53 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerOrdersProducts](
	[OrderID] [int] NOT NULL,
	[ProductID] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
	[Comments] [varchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customers]    Script Date: 10/31/2023 11:52:53 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customers](
	[CustomerID] [int] IDENTITY(1,1) NOT NULL,
	[Username] [varchar](30) NOT NULL,
	[Name] [varchar](30) NOT NULL,
	[Phone] [varchar](15) NOT NULL,
	[Email] [text] NOT NULL,
 CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED 
(
	[CustomerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DeliveryStatusCode]    Script Date: 10/31/2023 11:52:53 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DeliveryStatusCode](
	[DeliveryStatusCode] [int] IDENTITY(1,1) NOT NULL,
	[StatusName] [varchar](20) NOT NULL,
	[Description] [text] NULL,
 CONSTRAINT [PK_DeliveryStatusCode] PRIMARY KEY CLUSTERED 
(
	[DeliveryStatusCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderStatusCodes]    Script Date: 10/31/2023 11:52:53 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderStatusCodes](
	[OrderStatusCode] [int] IDENTITY(1,1) NOT NULL,
	[StatusName] [varchar](20) NOT NULL,
	[Description] [text] NULL,
 CONSTRAINT [PK_OrderStatusCodes] PRIMARY KEY CLUSTERED 
(
	[OrderStatusCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PaymentMethod]    Script Date: 10/31/2023 11:52:53 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PaymentMethod](
	[PaymentMethodID] [int] IDENTITY(1,1) NOT NULL,
	[MethodName] [varchar](30) NOT NULL,
 CONSTRAINT [PK_PaymentMethod] PRIMARY KEY CLUSTERED 
(
	[PaymentMethodID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 10/31/2023 11:52:53 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[ProductID] [int] IDENTITY(1,1) NOT NULL,
	[SupplierID] [int] NOT NULL,
	[ProductTypeCode] [int] NOT NULL,
	[Name] [varchar](40) NOT NULL,
	[Price] [int] NOT NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductTypes]    Script Date: 10/31/2023 11:52:53 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductTypes](
	[ProductTypeCode] [int] IDENTITY(1,1) NOT NULL,
	[TypeName] [varchar](30) NOT NULL,
 CONSTRAINT [PK_ProductTypes] PRIMARY KEY CLUSTERED 
(
	[ProductTypeCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Suppliers]    Script Date: 10/31/2023 11:52:53 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Suppliers](
	[SupplierID] [int] IDENTITY(1,1) NOT NULL,
	[SupplierName] [varchar](30) NOT NULL,
 CONSTRAINT [PK_Suppliers] PRIMARY KEY CLUSTERED 
(
	[SupplierID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Addresses] ON 

INSERT [dbo].[Addresses] ([AddressID], [City], [PostCode], [CompleteAddress]) VALUES (1, N'Tehran', 234578, N'Shohada sqaure,Derakhshan st,No 12')
INSERT [dbo].[Addresses] ([AddressID], [City], [PostCode], [CompleteAddress]) VALUES (2, N'Tabriz', 891238, N'Taleghani Av,Babaei st,No 78')
INSERT [dbo].[Addresses] ([AddressID], [City], [PostCode], [CompleteAddress]) VALUES (3, N'Esfehan', 190238, N'Hamidi Av,Yari st,No 324')
INSERT [dbo].[Addresses] ([AddressID], [City], [PostCode], [CompleteAddress]) VALUES (4, N'Shiraz', 564789, N'Gholami st,Bahar st,No 123')
INSERT [dbo].[Addresses] ([AddressID], [City], [PostCode], [CompleteAddress]) VALUES (9, N'Hamedan', 234567, N'Nooraei st,No 1231')
INSERT [dbo].[Addresses] ([AddressID], [City], [PostCode], [CompleteAddress]) VALUES (10, N'Tehran', 1234567, N'Damavand st,NO 341')
SET IDENTITY_INSERT [dbo].[Addresses] OFF
GO
INSERT [dbo].[CustomerAddresses] ([CustomerID], [AddressID], [DateFrom], [DateTo]) VALUES (3, 1, CAST(N'2020-05-06T15:12:23.000' AS DateTime), NULL)
INSERT [dbo].[CustomerAddresses] ([CustomerID], [AddressID], [DateFrom], [DateTo]) VALUES (4, 2, CAST(N'2019-10-19T22:12:12.000' AS DateTime), NULL)
INSERT [dbo].[CustomerAddresses] ([CustomerID], [AddressID], [DateFrom], [DateTo]) VALUES (2, 3, CAST(N'2021-09-13T10:12:08.000' AS DateTime), NULL)
INSERT [dbo].[CustomerAddresses] ([CustomerID], [AddressID], [DateFrom], [DateTo]) VALUES (1, 4, CAST(N'2023-11-03T18:12:08.000' AS DateTime), NULL)
INSERT [dbo].[CustomerAddresses] ([CustomerID], [AddressID], [DateFrom], [DateTo]) VALUES (3, 9, CAST(N'2023-10-28T18:37:36.523' AS DateTime), CAST(N'2023-10-29T06:04:01.840' AS DateTime))
INSERT [dbo].[CustomerAddresses] ([CustomerID], [AddressID], [DateFrom], [DateTo]) VALUES (2, 10, CAST(N'2023-10-31T07:10:34.823' AS DateTime), CAST(N'2023-10-31T08:08:01.080' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[CustomerOrders] ON 

INSERT [dbo].[CustomerOrders] ([OrderID], [CustomerID], [DateOrderPlaced], [OrderPrice], [DateOrderPaid], [PaymentMethodID], [OrderStatusCode]) VALUES (23, 1, CAST(N'2023-10-31T08:11:50.600' AS DateTime), 460000, NULL, 1, 1)
INSERT [dbo].[CustomerOrders] ([OrderID], [CustomerID], [DateOrderPlaced], [OrderPrice], [DateOrderPaid], [PaymentMethodID], [OrderStatusCode]) VALUES (24, 2, CAST(N'2023-10-31T08:12:31.457' AS DateTime), 4887000, NULL, 1, 1)
INSERT [dbo].[CustomerOrders] ([OrderID], [CustomerID], [DateOrderPlaced], [OrderPrice], [DateOrderPaid], [PaymentMethodID], [OrderStatusCode]) VALUES (25, 6, CAST(N'2023-10-31T08:13:17.077' AS DateTime), 6201000, NULL, 1, 4)
SET IDENTITY_INSERT [dbo].[CustomerOrders] OFF
GO
INSERT [dbo].[CustomerOrdersDelivery] ([OrderID], [DateReported], [DeliveryStatusCode]) VALUES (25, CAST(N'2023-10-31T08:15:59.000' AS DateTime), 2)
GO
INSERT [dbo].[CustomerOrdersProducts] ([OrderID], [ProductID], [Quantity], [Comments]) VALUES (23, 1, 1, N'Good')
INSERT [dbo].[CustomerOrdersProducts] ([OrderID], [ProductID], [Quantity], [Comments]) VALUES (23, 2, 1, N'Nice')
INSERT [dbo].[CustomerOrdersProducts] ([OrderID], [ProductID], [Quantity], [Comments]) VALUES (24, 5, 9, N'Good')
INSERT [dbo].[CustomerOrdersProducts] ([OrderID], [ProductID], [Quantity], [Comments]) VALUES (24, 6, 9, N'Beutiful design')
INSERT [dbo].[CustomerOrdersProducts] ([OrderID], [ProductID], [Quantity], [Comments]) VALUES (25, 8, 3, N'Suitable price')
INSERT [dbo].[CustomerOrdersProducts] ([OrderID], [ProductID], [Quantity], [Comments]) VALUES (25, 4, 3, N'High quality')
GO
SET IDENTITY_INSERT [dbo].[Customers] ON 

INSERT [dbo].[Customers] ([CustomerID], [Username], [Name], [Phone], [Email]) VALUES (1, N'HMohammadi', N'Hamid Mohammadi', N'09123456781', N'HMohammadi23@gmail.com')
INSERT [dbo].[Customers] ([CustomerID], [Username], [Name], [Phone], [Email]) VALUES (2, N'Sara', N'Sara babaei', N'09347890123', N'Sara123@gmail.com')
INSERT [dbo].[Customers] ([CustomerID], [Username], [Name], [Phone], [Email]) VALUES (3, N'Nima', N'Nima hashemi', N'09234576781', N'NHashemi12@gmail.com')
INSERT [dbo].[Customers] ([CustomerID], [Username], [Name], [Phone], [Email]) VALUES (4, N'Babak', N'Babak salehi', N'09189981234', N'Babaksaleh67@gmail.com')
INSERT [dbo].[Customers] ([CustomerID], [Username], [Name], [Phone], [Email]) VALUES (5, N'AhVaezy', N'AhmadVaezy', N'09345678791', N'Ahmad21@gmail.com')
INSERT [dbo].[Customers] ([CustomerID], [Username], [Name], [Phone], [Email]) VALUES (6, N'Omid', N'OmidHosseini', N'09145678792', N'OmidH@gmail.com')
INSERT [dbo].[Customers] ([CustomerID], [Username], [Name], [Phone], [Email]) VALUES (8, N'Mina', N'Mina sadati', N'09127891234', N'Mina34@Gmail.com')
SET IDENTITY_INSERT [dbo].[Customers] OFF
GO
SET IDENTITY_INSERT [dbo].[DeliveryStatusCode] ON 

INSERT [dbo].[DeliveryStatusCode] ([DeliveryStatusCode], [StatusName], [Description]) VALUES (1, N'Sending', N'The Order is sending to the customer')
INSERT [dbo].[DeliveryStatusCode] ([DeliveryStatusCode], [StatusName], [Description]) VALUES (2, N'Delivered', N'The Order is delivered by customer')
SET IDENTITY_INSERT [dbo].[DeliveryStatusCode] OFF
GO
SET IDENTITY_INSERT [dbo].[OrderStatusCodes] ON 

INSERT [dbo].[OrderStatusCodes] ([OrderStatusCode], [StatusName], [Description]) VALUES (1, N'Shopping', N'The customer is still choosing products')
INSERT [dbo].[OrderStatusCodes] ([OrderStatusCode], [StatusName], [Description]) VALUES (2, N'PayOrder', N'The customer pays for the order')
INSERT [dbo].[OrderStatusCodes] ([OrderStatusCode], [StatusName], [Description]) VALUES (3, N'Packing', N'The order is being ready')
INSERT [dbo].[OrderStatusCodes] ([OrderStatusCode], [StatusName], [Description]) VALUES (4, N'Delivery', N'The order is about to send')
SET IDENTITY_INSERT [dbo].[OrderStatusCodes] OFF
GO
SET IDENTITY_INSERT [dbo].[PaymentMethod] ON 

INSERT [dbo].[PaymentMethod] ([PaymentMethodID], [MethodName]) VALUES (1, N'OnSpot')
INSERT [dbo].[PaymentMethod] ([PaymentMethodID], [MethodName]) VALUES (2, N'Online')
SET IDENTITY_INSERT [dbo].[PaymentMethod] OFF
GO
SET IDENTITY_INSERT [dbo].[Products] ON 

INSERT [dbo].[Products] ([ProductID], [SupplierID], [ProductTypeCode], [Name], [Price]) VALUES (1, 1, 1, N'Nitro5', 120000)
INSERT [dbo].[Products] ([ProductID], [SupplierID], [ProductTypeCode], [Name], [Price]) VALUES (2, 2, 1, N'Mac2', 340000)
INSERT [dbo].[Products] ([ProductID], [SupplierID], [ProductTypeCode], [Name], [Price]) VALUES (3, 3, 3, N'Pack of 12 spoons', 540500)
INSERT [dbo].[Products] ([ProductID], [SupplierID], [ProductTypeCode], [Name], [Price]) VALUES (4, 4, 4, N'Tennis shoe', 567000)
INSERT [dbo].[Products] ([ProductID], [SupplierID], [ProductTypeCode], [Name], [Price]) VALUES (5, 2, 2, N'Mac3', 243000)
INSERT [dbo].[Products] ([ProductID], [SupplierID], [ProductTypeCode], [Name], [Price]) VALUES (6, 5, 1, N'Galaxy s23', 300000)
INSERT [dbo].[Products] ([ProductID], [SupplierID], [ProductTypeCode], [Name], [Price]) VALUES (7, 6, 1, N'Z365', 239000)
INSERT [dbo].[Products] ([ProductID], [SupplierID], [ProductTypeCode], [Name], [Price]) VALUES (8, 6, 1, N'D3400', 1500000)
SET IDENTITY_INSERT [dbo].[Products] OFF
GO
SET IDENTITY_INSERT [dbo].[ProductTypes] ON 

INSERT [dbo].[ProductTypes] ([ProductTypeCode], [TypeName]) VALUES (1, N'Electronics')
INSERT [dbo].[ProductTypes] ([ProductTypeCode], [TypeName]) VALUES (2, N'Computers')
INSERT [dbo].[ProductTypes] ([ProductTypeCode], [TypeName]) VALUES (3, N'Home and Kitchen')
INSERT [dbo].[ProductTypes] ([ProductTypeCode], [TypeName]) VALUES (4, N'Sports and Outdoors')
SET IDENTITY_INSERT [dbo].[ProductTypes] OFF
GO
SET IDENTITY_INSERT [dbo].[Suppliers] ON 

INSERT [dbo].[Suppliers] ([SupplierID], [SupplierName]) VALUES (1, N'Acer')
INSERT [dbo].[Suppliers] ([SupplierID], [SupplierName]) VALUES (2, N'Apple')
INSERT [dbo].[Suppliers] ([SupplierID], [SupplierName]) VALUES (3, N'Ikea')
INSERT [dbo].[Suppliers] ([SupplierID], [SupplierName]) VALUES (4, N'Nike')
INSERT [dbo].[Suppliers] ([SupplierID], [SupplierName]) VALUES (6, N'Nikon')
INSERT [dbo].[Suppliers] ([SupplierID], [SupplierName]) VALUES (5, N'Samsung')
SET IDENTITY_INSERT [dbo].[Suppliers] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Customers]    Script Date: 10/31/2023 11:52:53 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Customers] ON [dbo].[Customers]
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Products]    Script Date: 10/31/2023 11:52:53 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Products] ON [dbo].[Products]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Suppliers]    Script Date: 10/31/2023 11:52:53 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Suppliers] ON [dbo].[Suppliers]
(
	[SupplierName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[CustomerAddresses]  WITH CHECK ADD  CONSTRAINT [FK_CustomerAddresses_Addresses] FOREIGN KEY([AddressID])
REFERENCES [dbo].[Addresses] ([AddressID])
GO
ALTER TABLE [dbo].[CustomerAddresses] CHECK CONSTRAINT [FK_CustomerAddresses_Addresses]
GO
ALTER TABLE [dbo].[CustomerAddresses]  WITH CHECK ADD  CONSTRAINT [FK_CustomerAddresses_Customers] FOREIGN KEY([CustomerID])
REFERENCES [dbo].[Customers] ([CustomerID])
GO
ALTER TABLE [dbo].[CustomerAddresses] CHECK CONSTRAINT [FK_CustomerAddresses_Customers]
GO
ALTER TABLE [dbo].[CustomerOrders]  WITH CHECK ADD  CONSTRAINT [FK_CustomerOrders_Customers] FOREIGN KEY([CustomerID])
REFERENCES [dbo].[Customers] ([CustomerID])
GO
ALTER TABLE [dbo].[CustomerOrders] CHECK CONSTRAINT [FK_CustomerOrders_Customers]
GO
ALTER TABLE [dbo].[CustomerOrders]  WITH CHECK ADD  CONSTRAINT [FK_CustomerOrders_OrderStatusCodes] FOREIGN KEY([OrderStatusCode])
REFERENCES [dbo].[OrderStatusCodes] ([OrderStatusCode])
GO
ALTER TABLE [dbo].[CustomerOrders] CHECK CONSTRAINT [FK_CustomerOrders_OrderStatusCodes]
GO
ALTER TABLE [dbo].[CustomerOrders]  WITH CHECK ADD  CONSTRAINT [FK_CustomerOrders_PaymentMethod] FOREIGN KEY([PaymentMethodID])
REFERENCES [dbo].[PaymentMethod] ([PaymentMethodID])
GO
ALTER TABLE [dbo].[CustomerOrders] CHECK CONSTRAINT [FK_CustomerOrders_PaymentMethod]
GO
ALTER TABLE [dbo].[CustomerOrdersDelivery]  WITH CHECK ADD  CONSTRAINT [FK_CustomerOrdersDelivery_CustomerOrders] FOREIGN KEY([OrderID])
REFERENCES [dbo].[CustomerOrders] ([OrderID])
GO
ALTER TABLE [dbo].[CustomerOrdersDelivery] CHECK CONSTRAINT [FK_CustomerOrdersDelivery_CustomerOrders]
GO
ALTER TABLE [dbo].[CustomerOrdersDelivery]  WITH CHECK ADD  CONSTRAINT [FK_CustomerOrdersDelivery_DeliveryStatusCode] FOREIGN KEY([DeliveryStatusCode])
REFERENCES [dbo].[DeliveryStatusCode] ([DeliveryStatusCode])
GO
ALTER TABLE [dbo].[CustomerOrdersDelivery] CHECK CONSTRAINT [FK_CustomerOrdersDelivery_DeliveryStatusCode]
GO
ALTER TABLE [dbo].[CustomerOrdersProducts]  WITH CHECK ADD  CONSTRAINT [FK_CustomerOrdersProducts_CustomerOrders] FOREIGN KEY([OrderID])
REFERENCES [dbo].[CustomerOrders] ([OrderID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CustomerOrdersProducts] CHECK CONSTRAINT [FK_CustomerOrdersProducts_CustomerOrders]
GO
ALTER TABLE [dbo].[CustomerOrdersProducts]  WITH CHECK ADD  CONSTRAINT [FK_CustomerOrdersProducts_Products] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Products] ([ProductID])
GO
ALTER TABLE [dbo].[CustomerOrdersProducts] CHECK CONSTRAINT [FK_CustomerOrdersProducts_Products]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Products_ProductTypes] FOREIGN KEY([ProductTypeCode])
REFERENCES [dbo].[ProductTypes] ([ProductTypeCode])
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_ProductTypes]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Products_Suppliers] FOREIGN KEY([SupplierID])
REFERENCES [dbo].[Suppliers] ([SupplierID])
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_Suppliers]
GO
/****** Object:  StoredProcedure [dbo].[addAddress]    Script Date: 10/31/2023 11:52:53 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[addAddress] @CustomerName varchar(30),@City varchar(30),@PostCode int,@CompleteAddress varchar(Max), @Success bit Output,@Message varchar(50) Output
AS
Begin
	Declare @AddressID int;
	Declare @CustomerId int;
	Begin Transaction;
	Save Transaction SavePoint;

	Begin Try
		Insert into dbo.Addresses Values (@City,@PostCode,@CompleteAddress);
		Set @AddressId = (select Max(Addresses.AddressID) from dbo.Addresses);
		Set @CustomerId = (select Customers.CustomerID from Customers where Username = @CustomerName);
		Insert Into dbo.CustomerAddresses Values (@CustomerId,@AddressID,GETUTCDATE(),null);
		Commit Transaction
		Set @Success = 1;
		Set @Message = 'Transaction is commited successfully'; 
	End Try
	Begin Catch
		if @@TRANCOUNT > 0
			Begin
				Set @Success = 0;
				Set @Message = 'Transaction is not commited properly';
				Rollback Transaction SavePoint;
			End
	End Catch
End;
GO
USE [master]
GO
ALTER DATABASE [Store] SET  READ_WRITE 
GO
