USE [560Project]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE OR ALTER PROCEDURE [Flights].[IsUserInRole]
	@RoleName NVARCHAR(256),
	@UserId INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT COUNT(*) FROM [Flights].[ApplicationUserRole] AUR
	INNER JOIN [Flights].[ApplicationRole] AR ON AR.Id = AUR.RoleId 
	WHERE [UserId] = @UserId AND [Name] = @RoleName
END
GO