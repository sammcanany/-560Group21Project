/*Gets number of Tickets sold*/
USE [560Project]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE OR ALTER PROCEDURE [Flights].[FlightSearch]
	@DepartureDate DATE
AS
BEGIN
	SET NOCOUNT ON;
	SELECT 
		COUNT(F.[SeatsTaken]) AS N'Number Of SeatsTaken', 
		A.[Name],
		F.[DepartureDate],
		F.[DepartureTime],
		DEP.[Name] AS AirPortName
	FROM [Flights].[Flight] F
	INNER JOIN [Flights].[Airport] DEP ON DEP.[AirportID] = F.[DepartingAirportID]
	INNER JOIN [Flights].[Airport] DEST ON DEST.[AirportID] = F.[DestinationAirportID]
	INNER JOIN [Flights].[Airline] A ON A.[AirlineID] = F.[AirlineID]
	WHERE F.[DepartureDate] = @DepartureDate
	GROUP BY A.[Name],
		F.[DepartureDate],
		F.[DepartureTime],
		DEP.[Name]
END
GO