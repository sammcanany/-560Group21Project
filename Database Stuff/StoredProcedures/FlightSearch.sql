-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].FlightSearch 
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
	SELECT * FROM [dbo].[Flight]
END
GO
