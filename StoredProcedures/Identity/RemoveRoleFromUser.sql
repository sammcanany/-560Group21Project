USE [560Project]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE OR ALTER PROCEDURE [Flights].[RemoveRoleFromUser]
	@Name  NVARCHAR(256),
	@NormalizedName  NVARCHAR(256),
	@UserId INT
AS
BEGIN
	SET NOCOUNT ON;
	DELETE AUR
	FROM [Flights].[ApplicationUserRole] AUR
	INNER JOIN [Flights].[ApplicationRole] AR ON AR.[Id] = AUR.RoleId
	WHERE [UserId] = @userId
		AND [NormalizedName] = @NormalizedName
END
GO