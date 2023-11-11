create function dbo.GetYears (@Time DateTime)
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
Go
print dbo.GetYears(CONVERT(DATETIME, '2019-08-15', 102));
print dbo.GetYears(CONVERT(DATETIME, '2012-08-15', 102));
print dbo.GetYears(CONVERT(DATETIME, '2010-08-15', 102));