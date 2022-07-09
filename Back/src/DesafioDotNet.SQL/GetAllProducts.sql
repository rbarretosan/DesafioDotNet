USE [DesafioDotNet]
GO

/****** Object:  StoredProcedure [dbo].[GetAllProducts]    Script Date: 09/07/2022 14:48:34 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetAllProducts]
AS

SELECT * FROM Products

RETURN
GO