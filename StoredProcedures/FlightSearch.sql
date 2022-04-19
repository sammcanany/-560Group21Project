SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Flights].[FlightSearch] 
	-- Add the parameters for the stored procedure here
	@FromLocation CHAR(3),
	@ToLocation CHAR(3),
	@SeatsRequired INT,
	@DepartureDate DATE
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- TODO: update SELECT statement to return all from flight where:
	--DepartingAirportID == the AirportID of the code given by @FromLocation
	--DestinationAirportID == the AirportID of the code given by @ToLocation
	--Capacity >= @SeatsRequired
	--DepartureDate == @DepartureDate
	SELECT 
		F.[FlightNumber],
		DEP.[AirportCode],
		DEST.[AirportCode],
		A.[Name],
		F.[DepartureDate],
		F.[DepartureTime],
		F.[ArrivalTime],
		[SeatsRemaining] = F.[Capacity] - F.[SeatsTaken],
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


