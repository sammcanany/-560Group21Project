USE [AirlineDB]
GO

/****** Object:  Table [dbo].[FlightClass]    Script Date: 4/1/2022 1:19:31 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FlightClass]') AND type in (N'U'))
DROP TABLE [dbo].[FlightClass]
GO

/****** Object:  Table [dbo].[TicketInfo]    Script Date: 4/1/2022 1:20:39 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TicketInfo]') AND type in (N'U'))
DROP TABLE [dbo].[TicketInfo]
GO

/****** Object:  Table [dbo].[Class]    Script Date: 4/1/2022 1:20:12 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Class]') AND type in (N'U'))
DROP TABLE [dbo].[Class]
GO

/****** Object:  Table [dbo].[AirportFlight]    Script Date: 4/1/2022 1:17:58 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AirportFlight]') AND type in (N'U'))
DROP TABLE [dbo].[AirportFlight]
GO

/****** Object:  Table [dbo].[Flight]    Script Date: 4/1/2022 1:16:33 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Flight]') AND type in (N'U'))
DROP TABLE [dbo].[Flight]
GO

/****** Object:  Table [dbo].[Airline]    Script Date: 4/1/2022 1:17:10 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Airline]') AND type in (N'U'))
DROP TABLE [dbo].[Airline]
GO

/****** Object:  Table [dbo].[Airport]    Script Date: 4/1/2022 1:18:38 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Airport]') AND type in (N'U'))
DROP TABLE [dbo].[Airport]
GO