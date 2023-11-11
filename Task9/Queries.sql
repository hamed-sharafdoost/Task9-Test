select Suppliers.SupplierName,Products.Name from Suppliers left join Products on Suppliers.SupplierID = Products.SupplierID

select count(*) as 'Number Of Customers' from Customers

select z.Username,avg(n.Price) as 'average price of bought products' from Customers as z right join 
(select c.Price,a.CustomerID from Products as c right join (select b.ProductID,O.CustomerID from CustomerOrders as o right join CustomerOrdersProducts as b on o.OrderID = b.OrderID) as a on c.ProductID = a.ProductID)
as n on z.CustomerID = n.CustomerID Group by z.Username



