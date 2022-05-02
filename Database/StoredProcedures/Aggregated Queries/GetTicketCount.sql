/*Gets number of Tickets sold*/
USE [560Project]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE OR ALTER PROCEDURE [Flights].[GetTicketCount]
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
	WHERE F.[DepartureDate] = N'2021-10-03'
	GROUP BY A.[Name],
		F.[DepartureDate],
		F.[DepartureTime],
		DEP.[Name]


		SELECT 
		A.[Name] AS AirlineName,
		DEP.[Name],
		MIN(F.[DepartureDate]) AS EarliestFlightDate,
		MIN(F.[DepartureTime]) AS EarliestFlightTime
	FROM [Flights].[Flight] F
	INNER JOIN [Flights].[Airport] DEP ON DEP.[AirportID] = F.[DepartingAirportID]
	INNER JOIN [Flights].[Airline] A ON A.[AirlineID] = F.[AirlineID]
	GROUP BY A.[Name], DEP.[Name], F.[DepartureDate]
	ORDER BY MIN(F.[DepartureDate])