USE [560Project]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE OR ALTER PROCEDURE [Flights].[CreateOrUpdateUser]
	@Id INT,
	@UserName NVARCHAR(256), 
	@NormalizedUserName NVARCHAR(256), 
	@Email NVARCHAR(256), 
	@NormalizedEmail NVARCHAR(256), 
	@EmailConfirmed BIT,
	@FirstName NVARCHAR(256),
	@LastName NVARCHAR(256),
	@Address NVARCHAR(256) = NULL, 
	@PasswordHash NVARCHAR(MAX), 
	@PhoneNumber NVARCHAR(50) = NULL, 
	@PhoneNumberConfirmed BIT
AS
BEGIN
	SET NOCOUNT ON;
	IF @ID = 0
	BEGIN
		INSERT INTO [ApplicationUser] ([UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed],[FirstName],[LastName],[Address], [PasswordHash], [PhoneNumber], [PhoneNumberConfirmed])
		VALUES (@UserName, @NormalizedUserName, @Email, @NormalizedEmail, @EmailConfirmed,@FirstName,@LastName,@Address, @PasswordHash, @PhoneNumber, @PhoneNumberConfirmed);
		SELECT CAST(SCOPE_IDENTITY() AS INT)
	END
	ELSE
		BEGIN
		UPDATE [ApplicationUser]
		SET [UserName] = @UserName,
			[NormalizedUserName] = @NormalizedUserName,
			[Email] = @Email,
			[NormalizedEmail] = @NormalizedEmail,
			[EmailConfirmed] = @EmailConfirmed,
			[PasswordHash] = @PasswordHash,
			[FirstName] = @FirstName,
			[LastName] = @LastName,
			[Address] = @Address,
			[PhoneNumber] = @PhoneNumber,
			[PhoneNumberConfirmed] = @PhoneNumberConfirmed
		WHERE [Id] = @Id
	END
END
GO


