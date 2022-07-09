USE [DesafioDotNet]
GO

/****** Object:  StoredProcedure [dbo].[GetProductById]    Script Date: 09/07/2022 14:48:57 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetProductById](@Id int)
AS

SELECT * FROM Products WHERE Id=@Id

RETURN
GO