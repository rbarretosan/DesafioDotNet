USE [DesafioDotNet]
GO

/****** Object:  StoredProcedure [dbo].[InsertProduct]    Script Date: 09/07/2022 14:49:12 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[InsertProduct]
(
@createdAt datetime2(7),
@name nvarchar(MAX),
@price decimal(18, 2),
@brand nvarchar(MAX),
@updatedAt datetime2(7)
)

AS
BEGIN
	SET NOCOUNT ON

	INSERT INTO Products(createdAt, name, price, brand, updatedAt) VALUES(@createdAt, @name, @price, @brand, @updatedAt)

	SELECT SCOPE_IDENTITY()
END
GO