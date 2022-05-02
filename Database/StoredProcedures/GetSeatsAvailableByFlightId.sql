USE [560Project]
GO
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE OR ALTER     PROCEDURE [Flights].[GetSeatsAvailableByFlightId]
	@FlightID INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT 
		T.[SeatNumber]
	FROM [Flights].[TicketInfo] T
	INNER JOIN [Flights].[Flight] F ON F.[FlightID] = T.[FlightID]
	WHERE F.[FlightID] = @FlightID
END
GO


