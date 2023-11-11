IF exists (Select 1 from sys.procedures where name = 'addAddress') drop procedure addAddress;
Go
create procedure addAddress @CustomerName varchar(30),@City varchar(30),@PostCode int,@CompleteAddress varchar(Max), @Success bit Output,@Message varchar(50) Output
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

--Declare @success bit;
--Declare @message varchar(50);
--Execute addAddress @CustomerName = 'Sara',@City = 'Tehran',@PostCode = 1234567,@CompleteAddress= 'Damavand st,NO 341',@Success = @success output,@Message = @message output;
--Print @message

--Declare @success bit;
--Declare @message varchar(50);
--Execute addAddress @CustomerName = 'Babak',@City = 'Esfehan',@PostCode = 834791,@CompleteAddress= 'Mira st,NO 521',@Success = @success output,@Message = @message output;
--Print @message

Declare @success bit;
Declare @message varchar(50);
Execute addAddress @CustomerName = 'Leila',@City = 'Tehran',@PostCode = 424124,@CompleteAddress= 'Pasdaran st,NO 23',@Success = @success output,@Message = @message output;
Print @message
