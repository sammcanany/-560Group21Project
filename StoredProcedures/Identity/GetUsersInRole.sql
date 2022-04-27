USE [560Project]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE OR ALTER PROCEDURE [Flights].[GetUsersInRole]
	@NormalizedName  NVARCHAR(256)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT U.* FROM [ApplicationUser] U
	INNER JOIN [ApplicationUserRole] UR ON ur.[UserId] = U.[Id]
	INNER JOIN [ApplicationRole] R ON R.[Id] = UR.[RoleId]
	WHERE R.[NormalizedName] = @NormalizedName;
END
GO