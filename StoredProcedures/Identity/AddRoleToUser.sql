USE [560Project]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE OR ALTER PROCEDURE [Flights].[AddRole]
	@Name  NVARCHAR(256),
	@NormalizedName  NVARCHAR(256),
	@UserId INT,

	@RoleId INT OUTPUT
AS
BEGIN
	SET NOCOUNT ON;
	IF NOT EXISTS(SELECT [Id] FROM [ApplicationRole] WHERE [NormalizedName] = @NormalizedName)
	BEGIN
		INSERT INTO [ApplicationRole]([Name], [NormalizedName])
		VALUES(@Name, @NormalizedName);
		SET @RoleId = SCOPE_IDENTITY();
	END
	ELSE
	BEGIN
		SELECT [Id] FROM [ApplicationRole] WHERE [NormalizedName] = @NormalizedName
		SET @RoleId = SCOPE_IDENTITY();
	END
	IF NOT EXISTS(SELECT 1 FROM [ApplicationUserRole] WHERE [UserId] = @UserId AND [RoleId] = @RoleId)
	BEGIN
		INSERT INTO [ApplicationUserRole]([UserId], [RoleId]) VALUES(@UserId, @RoleId)
	END
END
GO