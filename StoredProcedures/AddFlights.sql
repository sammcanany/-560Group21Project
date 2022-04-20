USE [560Project]
GO
DROP PROCEDURE [Flights].[AddFlights]
GO

DROP TYPE [Flights].[FlightTableType]
GO
CREATE TYPE [Flights].[FlightTableType] AS TABLE (
	FlightNumber NVARCHAR(6) NOT NULL,
	DepartingAirportCode NVARCHAR(3) NOT NULL,
	DestinationAirportCode NVARCHAR(3) NOT NULL,
	Airline NVARCHAR(64) NOT NULL,
	DepartureDate DATE NOT NULL,
	DepartureTime TIME NOT NULL,
	ArrivalTime TIME NOT NULL,
	Capacity INT NOT NULL,
	SeatsTaken INT NOT NULL,
	Price MONEY NOT NULL
	)

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Flights].[AddFlights]
	(@ImportTable [Flights].[FlightTableType] READONLY)
AS
BEGIN
	SET NOCOUNT ON;
	INSERT INTO [Flights].[Flight](FlightNumber, DepartingAirportID, DestinationAirportID, AirlineID, DepartureDate, DepartureTime, ArrivalTime, Capacity,SeatsTaken,Price)
	SELECT NF.[FlightNumber],DEP.[AirportID],DEST.[AirportID],A.[AirlineID],NF.[DepartureDate], NF.[DepartureTime], NF.[ArrivalTime], NF.[Capacity], NF.[SeatsTaken], NF.[Price]
	FROM @ImportTable NF
	INNER JOIN [Flights].[Airport] DEP ON DEP.[AirportCode] = NF.[DepartingAirportCode]
	INNER JOIN [Flights].[Airport] DEST ON DEST.[AirportCode] = NF.[DestinationAirportCode]
	INNER JOIN [Flights].[Airline] A ON A.[Name] = NF.[Airline]
END
GO
