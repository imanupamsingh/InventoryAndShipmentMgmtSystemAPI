USE [WebAPI_DB]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 01-01-2025 18:06:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[ProductID] [int] IDENTITY(1,1) NOT NULL,
	[ProductName] [varchar](100) NULL,
	[Price] [decimal](18, 2) NULL,
	[Quantity] [int] NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedOn] [datetime] NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Product] ON 

INSERT [dbo].[Product] ([ProductID], [ProductName], [Price], [Quantity], [CreatedOn], [UpdatedOn]) VALUES (1002, N'Watch', CAST(2000.00 AS Decimal(18, 2)), 1, CAST(N'2024-12-29T16:15:07.407' AS DateTime), CAST(N'2024-12-30T13:02:40.310' AS DateTime))
INSERT [dbo].[Product] ([ProductID], [ProductName], [Price], [Quantity], [CreatedOn], [UpdatedOn]) VALUES (1003, N'Laptop', CAST(60000.00 AS Decimal(18, 2)), 1, CAST(N'2024-12-29T16:22:14.963' AS DateTime), CAST(N'2025-01-01T16:30:14.080' AS DateTime))
INSERT [dbo].[Product] ([ProductID], [ProductName], [Price], [Quantity], [CreatedOn], [UpdatedOn]) VALUES (1004, N'Car', CAST(2000000.00 AS Decimal(18, 2)), 1, CAST(N'2024-12-29T19:37:57.357' AS DateTime), NULL)
INSERT [dbo].[Product] ([ProductID], [ProductName], [Price], [Quantity], [CreatedOn], [UpdatedOn]) VALUES (1007, N'Truck', CAST(3532340.00 AS Decimal(18, 2)), 2, CAST(N'2024-12-29T19:43:51.420' AS DateTime), CAST(N'2025-01-01T16:49:52.200' AS DateTime))
INSERT [dbo].[Product] ([ProductID], [ProductName], [Price], [Quantity], [CreatedOn], [UpdatedOn]) VALUES (1021, N'Bike', CAST(10000.00 AS Decimal(18, 2)), 11, CAST(N'2025-01-01T15:43:11.530' AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[Product] OFF
GO
/****** Object:  StoredProcedure [dbo].[spDeleteProduct]    Script Date: 01-01-2025 18:06:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create proc [dbo].[spDeleteProduct]
(
@ProductId INT,
@IsSuccess bit out,
@IsError bit out,
@ErrorMsg Varchar(50) out
)
As
begin
     Delete from Product 
	 where ProductId=@ProductId

	  set @IsSuccess=1
	  set @IsError=1
	  set @ErrorMsg='Product details has been deleted successfully!'

end
GO
/****** Object:  StoredProcedure [dbo].[spGetAllProducts]    Script Date: 01-01-2025 18:06:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create proc [dbo].[spGetAllProducts]
As
Begin
     Select * from Product (nolock)
end
GO
/****** Object:  StoredProcedure [dbo].[spGetProductById]    Script Date: 01-01-2025 18:06:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create proc [dbo].[spGetProductById]
(
@ProductId INT
)
As
begin
	Select * from Product (nolock) 
	where ProductId=@ProductId
end
GO
/****** Object:  StoredProcedure [dbo].[spSaveProduct]    Script Date: 01-01-2025 18:06:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--sp_helptext spSaveProduct

CREATE proc [dbo].[spSaveProduct] 
( @ProductName Varchar(100), 
@Quantity INT, 
@Price DECIMAL(18, 2), 
@IsSuccess bit out, 
@IsError bit out, 
@ErrorMsg Varchar(50) out ) 
As begin           
insert into Product(ProductName,Quantity,Price,CreatedOn)    
values (
Isnull(@ProductName,''),isnull(@Quantity,0),isnull(@Price,0.0),GETDATE())     
set @IsSuccess=1    
set @IsError=1    
set @ErrorMsg='Product details has been added successfully!' 
end


GO
/****** Object:  StoredProcedure [dbo].[spUpdateProduct]    Script Date: 01-01-2025 18:06:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create proc [dbo].[spUpdateProduct]
(
@ProductId INT,
@ProductName Varchar(100),
@Quantity INT,
@Price DECIMAL(18, 2),
@IsSuccess bit out,
@IsError bit out,
@ErrorMsg Varchar(50) out
)
As
begin
     Update Product
	 set ProductName=ISNULL(@ProductName,''),
	 Quantity=ISNULL(@Quantity,0),
	 Price=ISNULL(@Price,0.0),
	 UpdatedOn=GETDATE()
	 where ProductId=@ProductId

	  set @IsSuccess=1
	  set @IsError=1
	  set @ErrorMsg='Product details has been updated successfully!'


end 
GO
