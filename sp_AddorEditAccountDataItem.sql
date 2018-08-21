SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF EXISTS (SELECT 1 FROM sys.procedures WHERE Name = 'sp_AddorEditAccountDataItem')
BEGIN
	DROP PROCEDURE sp_AddorEditAccountDataItem
END

GO

CREATE PROCEDURE sp_AddorEditAccountDataItem(@Name AS NVARCHAR(256), @Website AS NVARCHAR(256), @Username AS NVARCHAR(256), @Password AS NVARCHAR(256),
											 @Passcode AS NVARCHAR(256), @Other AS NVARCHAR(256)) AS
BEGIN

	--if this account name does not exist , add it
	IF NOT EXISTS(SELECT 1 FROM Accounts WHERE Name = @Name)
	BEGIN
		INSERT INTO Accounts VALUES(@Name, @Website, @Username, @Password, @Passcode, @Other)
	END
	ELSE
	BEGIN
		UPDATE Accounts SET [Website] = @Website, [Username] = @Username, [Password] = @Password,
			[Passcode] = @Passcode, [Other] = @Other
		WHERE Name = @Name
	END
END