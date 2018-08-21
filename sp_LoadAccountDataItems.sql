SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF EXISTS (SELECT 1 FROM sys.procedures WHERE Name = 'sp_LoadAccountDataItems')
BEGIN
	DROP PROCEDURE sp_LoadAccountDataItems
END

GO

CREATE PROCEDURE sp_LoadAccountDataItems AS
BEGIN

	SELECT [Name], [Website], [Username], [Password], [Passcode], [Other]
	FROM Accounts
END