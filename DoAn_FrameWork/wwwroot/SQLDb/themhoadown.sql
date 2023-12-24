DECLARE @counter INT = 1
DECLARE @order_id INT

WHILE @counter <= 20
BEGIN
    -- T?o m?t ??n hàng m?i
    INSERT INTO [dbo].[orders] ([customer_id], [shipping_id], [payment_id], [order_total], [order_status], [created_at])
    VALUES (
        ROUND(RAND() * 9 + 1, 0), -- Random customer_id t? 1 ??n 10
        ROUND(RAND() * 4 + 1, 0), -- Random shipping_id t? 1 ??n 5
        ROUND(RAND() * 4 + 1, 0), -- Random payment_id t? 1 ??n 5
        ROUND(RAND() * 1000, 0), -- Random order_total t? 0 ??n 1000
        CASE ROUND(RAND() * 4, 0)
            WHEN 0 THEN 'Pending'
            WHEN 1 THEN 'Confirmed'
            WHEN 2 THEN 'Shipped'
            WHEN 3 THEN 'Cancelled'
            ELSE 'Done'
        END,
        DATEADD(day, -ROUND(RAND() * 365, 0), GETDATE()) -- Random ngày trong n?m qua
    )

    SET @order_id = SCOPE_IDENTITY() -- L?y order_id v?a t?o

    -- T?o 2 ho?c 3 order details cho m?i ??n hàng
    DECLARE @details_count INT = 3 -- Random t? 2 ??n 3 order details
    DECLARE @detail_counter INT = 1

    WHILE @detail_counter <= @details_count
    BEGIN
        INSERT INTO [dbo].[order_details] ([order_id], [product_id], [product_sales_quantity])
        VALUES (
            @order_id,
            ROUND(RAND() * 19 + 60, 0), -- Random product_id t? 60 ??n 79
            ROUND(RAND() * 5 + 1, 0) -- Random product_sales_quantity t? 1 ??n 5
        )

        SET @detail_counter = @detail_counter + 1
    END

    SET @counter = @counter + 1
END


ALTER TABLE [dbo].[orders]
ALTER COLUMN [order_status] NVARCHAR(300); 