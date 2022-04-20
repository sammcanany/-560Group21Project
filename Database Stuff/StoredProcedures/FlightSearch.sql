SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [Flight].FlightSearch 
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
	SELECT * FROM [dbo].[Flight] F
	WHERE F.[
END
GO
