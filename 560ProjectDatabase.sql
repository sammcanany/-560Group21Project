-- Drops '560Project' database if it already exists
IF EXISTS
   (
      SELECT *
      FROM sys.databases d
      WHERE d.name = N'`$(560Project)'
   )
BEGIN
   ALTER DATABASE [`$(560Project)]
   SET SINGLE_USER
   WITH ROLLBACK IMMEDIATE;

   DROP DATABASE [`$(560Project)];
END;x

-- Creates database automatically, after dropping it if it already existed
USE [master];

IF EXISTS
   (
      SELECT *
      FROM sys.databases d
      WHERE d.name = N'$(560Project)'
   )
BEGIN
   DECLARE @Msg varchar(256) = 'Database [$(560Project)] already exists.';
   PRINT @Msg;
   RETURN;
END;

-- The file has to be provided to work around a known bug
-- with SqlLocalDB 2017.
CREATE DATABASE [$(560Project)]
ON PRIMARY 
(
   NAME = N'PrimaryData',
   FILENAME = N'$(jayja)\$(560Project).mdf'
)
COLLATE SQL_Latin1_General_CP1_CI_AS;

ALTER DATABASE [$(560Project)]
SET
   ANSI_NULLS ON,
   ANSI_PADDING ON,
   ANSI_WARNINGS ON,
   ARITHABORT ON,
   AUTO_CLOSE OFF,
   AUTO_CREATE_STATISTICS ON,
   AUTO_SHRINK OFF,
   AUTO_UPDATE_STATISTICS ON,
   CONCAT_NULL_YIELDS_NULL ON,
   NUMERIC_ROUNDABORT OFF,
   QUOTED_IDENTIFIER OFF,
   RECURSIVE_TRIGGERS OFF,
   ALLOW_SNAPSHOT_ISOLATION ON,
   RECOVERY SIMPLE;
GO

-- Creates schema if it doesn't already exist
IF SCHEMA_ID(N'Flights') IS NULL
	EXEC(N'CREATE SCHEMA [Flights];');
GO

DROP TABLE IF EXISTS Flights.TicketInfo
DROP TABLE IF EXISTS Flights.PassengerProfile
DROP TABLE IF EXISTS Flights.FlightClass
DROP TABLE IF EXISTS Flights.Class
DROP TABLE IF EXISTS Flights.Flight
DROP TABLE IF EXISTS Flights.Airline
DROP TABLE IF EXISTS Flights.Airport
GO

-- Checks that table doesn't already exist, then creates it
IF OBJECT_ID(N'Flights.Airport') IS NULL
BEGIN
	CREATE TABLE Flights.Airport
	(
		AirportID INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
		Abbreviation NVARCHAR(32) NOT NULL UNIQUE,
		Name NVARCHAR(64) NOT NULL UNIQUE,
		AirportCode INT NOT NULL UNIQUE,
		Location NVARCHAR(128) NOT NULL
	);
END
INSERT INTO Flights.Airport(AirportID, Abbreviation, [Name], AirportCode, [Location])
VALUES
	('1', 'ATL','Hartsfield-Jackson International Airport', '1000', 'GA'),
	('2', 'DFW','Dallas/Fort Worth International Airport', '2000', 'TX'),
	('3', 'DEN','Denver International Airport','3000','CO'),
	('4', 'ORD','O''Hare International Airport','4000','IL'),
	('5', 'LAX','Los Angeles International Airport','5000','CA'),
	('6', 'CLT','Charlotte Douglas International Airport','6000','NC'),
	('7', 'LAS','Harry Reid International Airport','7000','NV'),
	('8', 'PHX','Phoenix Sky Harbor International Airport','8000', 'AZ'),
	('9', 'MCO','Orlando International Airport','9000', 'FL'),
	('10', 'MHK','Manhattan Regional Airport','9001', 'KS');
GO

-- Checks that table doesn't already exist, then creates it
IF OBJECT_ID(N'Flights.Airline') IS NULL
BEGIN
	CREATE TABLE Flights.Airline
	(
		AirlineID INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
		Name NVARCHAR(64) NOT NULL UNIQUE,
		Country NVARCHAR(64) NOT NULL
	);
END
INSERT INTO Flights.Airline(AirlineID, [Name], Country)
VALUES
	('1', 'Southwest Airlines', 'United States of America'),
	('2', 'American Airlines', 'United States of America'),
	('3', 'Spirit Airlines', 'United States of America'),
	('4', 'Delta Air Lines', 'United States of America'),
	('5', 'United Airlines', 'United States of America'),
	('6', 'Allegiant Air', 'United States of America')

-- Checks that table doesn't already exist, then creates it
IF OBJECT_ID(N'Flights.Flight') IS NULL
BEGIN
	CREATE TABLE Flights.Flight
	(
		FlightID INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
		FlightNumber INT NOT NULL UNIQUE,
		DepartingAirportID INT NOT NULL FOREIGN KEY REFERENCES Flights.Airport(AirportID),
		DestinationAirportID INT NOT NULL FOREIGN KEY REFERENCES Flights.Airport(AirportID),
		AirlineID INT NOT NULL FOREIGN KEY REFERENCES Flights.Airline(AirlineID),
		DepartureDate DATE NOT NULL,
		DepartureTime TIME NOT NULL,
		ArrivalTime TIME NOT NULL,
		Capacity INT NOT NULL
	);
END
INSERT INTO Flights.Flight(FlightID, FlightNumber, DepartingAirportID, DestinationAirportID, AirlineID, DepartureDate, DepartureTime, ArrivalTime, Capacity)
VALUES
	(1, 'IGTGD', '1', '2', 'AIRLINE', '2021-10-03','1:52:00',261),
	(2, 'JTRLT', '1', '3', 'AIRLINE', '2023-02-07','23:08:00',262),
	(3, 'JQOWL', '1', '4', 'AIRLINE', '2021-12-08','12:22:00',147),
	(4, 'PJVFY', '1', '5', 'AIRLINE', '2022-10-18','10:51:00',223),
	(5, 'MUNNG', '1', '6', 'AIRLINE', '2023-01-04','18:31:00',231),
	(6, 'LYMXL', '1', '7', 'AIRLINE', '2022-11-21','11:54:00',238),
	(7, 'NOBCK', '1', '8', 'AIRLINE', '2021-09-04','8:44:00',290),
	(8, 'LTYWK', '1', '9', 'AIRLINE', '2022-04-17','5:23:00',135),
	(9, 'ZJTBB', '1', '10', 'AIRLINE', '2022-05-13','19:52:00',252)

-- Checks that table doesn't already exist, then creates it
IF OBJECT_ID(N'Flights.Class') IS NULL
BEGIN
	CREATE TABLE Flights.Class
	(
		ClassID INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
		Name NVARCHAR(32) NOT NULL UNIQUE
	);
END
INSERT INTO Flights.Class(ClassID, [Name])
VALUES
	('1', 'First Class'),
	('2', 'Business Class'),
	('3', 'Economy Class')

-- Checks that table doesn't already exist, then creates it
IF OBJECT_ID(N'Flights.FlightClass') IS NULL
BEGIN
	CREATE TABLE Flights.FlightClass
	(
		FlightID INT NOT NULL FOREIGN KEY REFERENCES Flights.Flight(FlightID),
		ClassID INT NOT NULL FOREIGN KEY REFERENCES Flights.Class(ClassID),
		Price INT NOT NULL

		PRIMARY KEY(FlightID, ClassID)
	);
END
INSERT INTO Flights.FlightClass(FLightID, ClassID, Price)
VALUES
	('1', '1', '1300'),
	('1', '2', '600'),
	('1', '3', '250'),
	('2', '1', '1200'),
	('2', '2', '500'),
	('2', '3', '200'),
	('3', '1', '1200'),
	('3', '2', '700'),
	('3', '3', '350')

-- Checks that table doesn't already exist, then creates it	
IF OBJECT_ID(N'Flights.PassengerProfile') IS NULL
BEGIN
CREATE TABLE Flights.PassengerProfile
	(
		ProfileID INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
		FirstName NVARCHAR(32) NOT NULL,
		LastName NVARCHAR(32) NOT NULL,
		EmailAddress NVARCHAR(64) NOT NULL UNIQUE,
		[Address] NVARCHAR(64) NOT NULL,
		[Password] NVARCHAR(32) NOT NULL,
		PhoneNumber NVARCHAR(32)
	);
END
INSERT INTO Flights.PassengerProfile(ProfileID, FirstName, LastName, EmailAdress, [Address], [Password], PhoneNumber)
VALUES
	(1,'Quamar','Roy','leo.cras@icloud.couk','P.O. Box 962, 9214 Magna. St.','LCS87NTV5ZF','(661) 858-9563'),
	(2,'Calista','Hill','malesuada@yahoo.couk','P.O. Box 559, 6066 Lectus Avenue','TKK81QBQ2OQ','(687) 183-5211'),
	(3,'Salvador','Huffman','hendrerit@hotmail.org','986-3229 Urna Street','NEL53LPV8UM','(356) 158-2672'),
	(4,'Slade','Martin','donec.tempus@outlook.org','Ap #278-4822 Nec St.','LKC66MVH5MO','(475) 562-3885'),
	(5,'Hollee','Pruitt','mollis.phasellus@outlook.ca','991-4470 Scelerisque St.','LCA83XSN9FQ','(644) 232-5558'),
	(6,'Hilda','Snow','lobortis.tellus@protonmail.org','389-8048 Dui. Road','EVF70IFJ4RW','(798) 663-9127'),
	(7,'Alexandra','Kaufman','nisi.aenean@protonmail.ca','P.O. Box 735, 7494 Pellentesque St.','GCM46NYH4JH','(547) 648-7568'),
	(8,'Jillian','Hicks','est.nunc@yahoo.couk','Ap #571-3597 Pellentesque Rd.','CDY76KXQ4LX','(741) 404-6778'),
	(9,'Drew','Henson','quisque@icloud.edu','P.O. Box 689, 5479 Proin Rd.','NLX82BNJ6IU','(618) 143-9354'),
	(10,'Jelani','Franks','a.neque@yahoo.ca','Ap #974-9581 Lacus, Street','JZP88JPQ7HP','(401) 214-5419');

-- Checks that table doesn't already exist, then creates it
IF OBJECT_ID(N'Flights.TicketInfo') IS NULL
BEGIN
	CREATE TABLE TicketInfo
	(
		TicketInfoID INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
		ProfileID INT NOT NULL FOREIGN KEY REFERENCES Flights.PassengerProfile(ProfileID),
		FlightID INT NOT NULL FOREIGN KEY REFERENCES Flights.Flight(FlightID),
		ClassID	INT NOT NULL FOREIGN KEY REFERENCES Flights.Class(ClassID),
		SeatNumber INT NOT NULL

		UNIQUE(FlightID, SeatNumber)
	);
END
INSERT INTO Flights.TicketInfo(TicketInfoID, ProfileID, FlightID, ClassID, SeatNumber)
VALUES
	('1', '1', '1', '3', '70'),
	('2', '2', '1', '3', '71'),
	('3', '3', '2', '3', '10'),
	('4', '4', '3', '3', '20'),
	('5', '5', '4', '3', '30'),
	('6', '6', '5', '3', '40'),
	('7', '7', '6', '3', '50'),
	('8', '8', '7', '3', '60'),
	('9', '9', '8', '3', '70'),
	('10', '10', '9', '3', '80')