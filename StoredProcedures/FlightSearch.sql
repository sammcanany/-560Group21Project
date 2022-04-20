USE [560Project]
GO
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE OR ALTER PROCEDURE [Flights].[FlightSearch]
	@FromLocation CHAR(3),
	@ToLocation CHAR(3),
	@SeatsRequired INT,
	@DepartureDate DATE
AS
BEGIN
	SET NOCOUNT ON;
	SELECT 
		F.[FlightID] AS N'Id',
		F.[FlightNumber],
		DEP.[AirportCode] AS N'DepartingAirportCode',
		DEST.[AirportCode] AS N'DestinationAirportCode',
		A.[Name] AS N'Airline',
		F.[DepartureDate],
		F.[DepartureTime],
		F.[ArrivalTime],
		F.[Capacity],
		F.[SeatsTaken],
		F.[Price]
	FROM [Flights].[Flight] F
	INNER JOIN [Flights].[Airport] DEP ON DEP.[AirportID] = F.[DepartingAirportID]
	INNER JOIN [Flights].[Airport] DEST ON DEST.[AirportID] = F.[DestinationAirportID]
	INNER JOIN [Flights].[Airline] A ON A.[AirlineID] = F.[AirlineID]
	WHERE DEP.[AirportCode] = @FromLocation
	AND DEST.[AirportCode] = @ToLocation
	AND (F.[Capacity] - F.[SeatsTaken]) >= @SeatsRequired
	AND F.[DepartureDate] = @DepartureDate
END
GO


