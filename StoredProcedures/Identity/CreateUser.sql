SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE OR ALTER PROCEDURE [Flights].[CreateUser]
	@UserName NVARCHAR(256), 
	@NormalizedUserName NVARCHAR(256), 
	@Email NVARCHAR(256), 
	@NormalizedEmail NVARCHAR(256), 
	@EmailConfirmed BIT, 
	@PasswordHash NVARCHAR(MAX), 
	@PhoneNumber NVARCHAR(50) = NULL, 
	@PhoneNumberConfirmed BIT
AS
BEGIN
	SET NOCOUNT ON;
	INSERT INTO [ApplicationUser] ([UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [PhoneNumber], [PhoneNumberConfirmed])
	VALUES (@UserName, @NormalizedUserName, @Email, @NormalizedEmail, @EmailConfirmed, @PasswordHash, @PhoneNumber, @PhoneNumberConfirmed);
	SELECT CAST(SCOPE_IDENTITY() AS INT)
END
GO



