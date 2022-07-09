USE [DesafioDotNet]
GO

/****** Object:  StoredProcedure [dbo].[UpdateProduct]    Script Date: 09/07/2022 14:49:23 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UpdateProduct]
(
@Id int,
@name nvarchar(MAX),
@price decimal(18, 2),
@brand nvarchar(MAX),
@updatedAt datetime2(7)
)

AS

UPDATE Products SET name=@name, price=@price, brand=@brand, @updatedAt=@updatedAt WHERE Id=@Id

RETURN
GO