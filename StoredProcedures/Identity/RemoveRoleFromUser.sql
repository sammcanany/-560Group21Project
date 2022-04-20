USE [560Project]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE OR ALTER PROCEDURE [Flights].[RemoveRole]
	@Name  NVARCHAR(256),
	@NormalizedName  NVARCHAR(256),
	@UserId INT,

	@RoleId INT OUTPUT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT [Id] FROM [ApplicationRole] WHERE [NormalizedName] = @NormalizedName;
	SET @RoleId = SCOPE_IDENTITY();
	IF EXISTS(SELECT [Id] FROM [ApplicationRole] WHERE [NormalizedName] = @NormalizedName)
	BEGIN
		DELETE FROM [ApplicationUserRole] WHERE [UserId] = @userId AND [RoleId] = @RoleId
	END
END
GO