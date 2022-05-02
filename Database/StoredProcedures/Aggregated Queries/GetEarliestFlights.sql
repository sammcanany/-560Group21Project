/*Gets number of Tickets sold*/
USE [560Project]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE OR ALTER PROCEDURE [Flights].[FlightSearch]
AS
BEGIN
	SET NOCOUNT ON;
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

END
GO

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
