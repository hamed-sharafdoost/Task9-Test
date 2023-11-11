USE [Store]
GO

/****** Object:  Trigger [dbo].[updateOrder]    Script Date: 11/10/2023 9:42:30 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TRIGGER [dbo].[updateOrder] ON [dbo].[CustomerOrdersDelivery] After Insert
AS BEGIN
	Declare @OrderID INT;
	Update CustomerOrders set OrderStatusCode = 4 where OrderID = (SELECT SUM(OrderID)  
      FROM inserted  
      WHERE CustomerOrders.OrderID  
       = inserted.OrderID);
END;
GO

ALTER TABLE [dbo].[CustomerOrdersDelivery] ENABLE TRIGGER [updateOrder]
GO


