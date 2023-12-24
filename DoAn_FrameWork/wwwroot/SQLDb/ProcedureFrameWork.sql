CREATE PROCEDURE GetTopFiveBestSellingProducts
AS
BEGIN
    SELECT TOP 5 
        p.product_name as ProductName,
		p.product_image as ProductImage,
        SUM(od.product_sales_quantity) AS TotalSales
    FROM products p
    INNER JOIN order_details od ON p.product_id = od.product_id
    GROUP BY 
        p.product_name,
		p.product_image
    ORDER BY 
        SUM(od.product_sales_quantity) DESC;
END
go
EXEC GetTopFiveBestSellingProducts;
go
CREATE PROCEDURE GetTopFiveCustomersWithMostOrders
AS
BEGIN
    SELECT TOP 5
        c.customer_name as CustomerName,
        c.customer_email as CustomerEmail,
        COUNT(o.order_id) AS TotalOrders
    FROM customers c
    INNER JOIN orders o ON c.customer_id = o.customer_id
    GROUP BY 
        c.customer_id,
        c.customer_name,
        c.customer_email
    ORDER BY COUNT(o.order_id) DESC;
END

go
EXEC GetTopFiveCustomersWithMostOrders;


SELECT MONTH(created_at) AS MonthValue, count(*) as NumOfOrder, sum(order_total) as MoneyEarn 
FROM orders 
where year(created_at) = year(getdate())
group by MONTH(created_at) , YEAR(created_at)


select MONTH(created_at) as MonthValue, count(customer_id) as NumberOfCustomer
from orders 
where year(created_at) = year(getdate())
group by MONTH(created_at)

