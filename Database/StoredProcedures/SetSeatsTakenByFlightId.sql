USE [560Project]
GO
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE OR ALTER     PROCEDURE [Flights].[SetSeatsTakenByFlightId]
	@FlightID INT
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE [Flights].[Flight]
	SET [SeatsTaken] = (
			SELECT COUNT(*)
			FROM [Flights].[TicketInfo]
			WHERE [FlightID] = @FlightID
			)
	WHERE [Flight].[FlightID] = @FlightID
END
GO


