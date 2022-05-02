USE [560Project]
GO
DROP PROCEDURE [Flights].[AddTickets]
GO

DROP TYPE [Flights].[TicketTableType]
GO
CREATE TYPE [Flights].[TicketTableType] AS TABLE (
	[ProfileID] INT NOT NULL,
	[FlightID] INT NOT NULL,
	[FirstName] NVARCHAR(32) NOT NULL,
	[LastName] NVARCHAR(32) NOT NULL,
	[SeatNumber] INT NOT NULL
	)
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Flights].[AddTickets]
	(@ImportTable [Flights].[TicketTableType] READONLY)
AS
BEGIN
	SET NOCOUNT ON;
	INSERT INTO [Flights].[TicketInfo](ProfileID, FlightID, FirstName, LastName, SeatNumber)
	SELECT *
	FROM @ImportTable NF
END
GO
