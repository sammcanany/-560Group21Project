USE [560Project]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE OR ALTER   PROCEDURE [Flights].[GetTicketByUserId]
	@ProfileID INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT 
		T.ProfileID,
		T.FlightID,
		T.FirstName,
		T.LastName,
		T.SeatNumber
	FROM [Flights].[TicketInfo] T
	WHERE T.[ProfileID] = @ProfileID
END
GO


