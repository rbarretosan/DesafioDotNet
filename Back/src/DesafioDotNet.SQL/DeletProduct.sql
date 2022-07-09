USE [DesafioDotNet]
GO

/****** Object:  StoredProcedure [dbo].[DeleteProduct]    Script Date: 09/07/2022 14:47:51 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[DeleteProduct](@Id int)

AS

DELETE FROM Products WHERE Id=@Id

RETURN
GO