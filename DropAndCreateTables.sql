USE [560Project]
GO
-- Creates schema if it doesn't already exist
IF SCHEMA_ID(N'Flights') IS NULL
	EXEC(N'CREATE SCHEMA [Flights];');
GO



DROP TABLE IF EXISTS Flights.TicketInfo
DROP TABLE IF EXISTS Flights.ApplicationUserRole
DROP TABLE IF EXISTS Flights.ApplicationUser
DROP TABLE IF EXISTS Flights.ApplicationRole
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
		AirportCode NVARCHAR(4) NOT NULL UNIQUE,
		[Name] NVARCHAR(64) NOT NULL UNIQUE,
		Location NVARCHAR(128) NOT NULL
	);
END
INSERT INTO Flights.Airport(AirportCode, [Name], [Location])
VALUES
	('ATL','Hartsfield-Jackson International Airport', 'GA'),
	('DFW','Dallas/Fort Worth International Airport', 'TX'),
	('DEN','Denver International Airport','CO'),
	('ORD','O''Hare International Airport','IL'),
	('LAX','Los Angeles International Airport','CA'),
	('CLT','Charlotte Douglas International Airport','NC'),
	('LAS','Harry Reid International Airport','NV'),
	('PHX','Phoenix Sky Harbor International Airport', 'AZ'),
	('MCO','Orlando International Airport', 'FL'),
	('MHK','Manhattan Regional Airport', 'KS');
GO

-- Checks that table doesn't already exist, then creates it
IF OBJECT_ID(N'Flights.Airline') IS NULL
BEGIN
	CREATE TABLE Flights.Airline
	(
		AirlineID INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
		[Name] NVARCHAR(64) NOT NULL UNIQUE,
		Country NVARCHAR(64) NOT NULL
	);
END
INSERT INTO Flights.Airline([Name], Country)
VALUES
	('Southwest Airlines', 'United States of America'),
	('American Airlines', 'United States of America'),
	('Spirit Airlines', 'United States of America'),
	('Delta Air Lines', 'United States of America'),
	('United Airlines', 'United States of America'),
	('Allegiant Air', 'United States of America');
GO

-- Checks that table doesn't already exist, then creates it
IF OBJECT_ID(N'Flights.Flight') IS NULL
BEGIN
	CREATE TABLE Flights.Flight
	(
		FlightID INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
		FlightNumber NVARCHAR(6) NOT NULL,
		DepartingAirportID INT NOT NULL FOREIGN KEY REFERENCES Flights.Airport(AirportID),
		DestinationAirportID INT NOT NULL FOREIGN KEY REFERENCES Flights.Airport(AirportID),
		AirlineID INT NOT NULL FOREIGN KEY REFERENCES Flights.Airline(AirlineID),
		DepartureDate DATE NOT NULL,
		DepartureTime TIME NOT NULL,
		ArrivalTime TIME NOT NULL,
		Capacity INT NOT NULL,
		SeatsTaken INT NOT NULL,
		Price MONEY NOT NULL
	);
END
INSERT INTO Flights.Flight(FlightNumber, DepartingAirportID, DestinationAirportID, AirlineID, DepartureDate, DepartureTime, ArrivalTime, Capacity,SeatsTaken,Price)
VALUES
	('IGTGD', '1', '2', 1, '2021-10-03','12:00:00','1:52:00',261,0,125),
	('JTRLT', '1', '3', 1, '2023-02-07','12:00:00','23:08:00',262,0,125),
	('JQOWL', '1', '4', 2, '2021-12-08','12:00:00','12:22:00',147,0,125),
	('PJVFY', '1', '5', 2, '2022-10-18','12:00:00','10:51:00',223,0,125),
	('MUNNG', '1', '6', 3, '2023-01-04','12:00:00','18:31:00',231,0,125),
	('LYMXL', '1', '7', 4, '2022-11-21','12:00:00','11:54:00',238,0,125),
	('NOBCK', '1', '8', 4, '2021-09-04','12:00:00','8:44:00',290,0,125),
	('LTYWK', '1', '9', 5, '2022-04-17','12:00:00','5:23:00',135,0,125),
	('ZJTBB', '1', '10', 6, '2022-05-13','12:00:00','19:52:00',252,0,125);
GO

-- Checks that table doesn't already exist, then creates it	
IF OBJECT_ID(N'Flights.ApplicationUser') IS NULL
BEGIN
CREATE TABLE Flights.[ApplicationUser]
	(
		[ID] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
		[UserName] NVARCHAR(256) NOT NULL UNIQUE,
		[NormalizedUserName] NVARCHAR(256) NOT NULL UNIQUE,
		[Email] NVARCHAR(256) NOT NULL UNIQUE,
		[NormalizedEmail] NVARCHAR(256) NOT NULL UNIQUE,
		[EmailConfirmed] BIT NOT NULL,
		[FirstName] NVARCHAR(32) NOT NULL,
		[LastName] NVARCHAR(32) NOT NULL,
		[Address] NVARCHAR(64) NULL,
		[PasswordHash] NVARCHAR(MAX) NOT NULL,
		[PhoneNumber] NVARCHAR(50) NULL,
		[PhoneNumberConfirmed] BIT NOT NULL
	);
END

CREATE INDEX [IX_ApplicationUser_NormalizedUserName] ON Flights.[ApplicationUser] ([NormalizedUserName])

GO

CREATE INDEX [IX_ApplicationUser_NormalizedEmail] ON Flights.[ApplicationUser] ([NormalizedEmail])

GO

INSERT INTO Flights.ApplicationUser([UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [FirstName], [LastName], [Address], [PasswordHash], [PhoneNumber], [PhoneNumberConfirmed])
VALUES
	('sammcanany@ksu.edu','SAMMCANANY@KSU.EDU','sammcanany@ksu.edu','SAMMCANANY@KSU.EDU',1,'Sam','McAnany','Address','AQAAAAEAACcQAAAAEAPABFsmgdk0oze/asltFwOWWRuNw7sGRVFHj/FIk2h2Rrm44S9v6BtsvDgKEkY+Rg==','(785) 555-5555',1),
	('leo.cras@icloud.couk','LEO.CRAS@ICLOUD.COUK','leo.cras@icloud.couk','LEO.CRAS@ICLOUD.COUK',1,'Quamar','Roy','P.O. Box 962, 9214 Magna. St.','AQAAAAEAACcQAAAAEC8drUhJkaVG1yRbw38h1gAw3wyXjAvha8zTCZ5eb1n5eE+mGzCFGdX9ypnJz6E2BA==','(661) 858-9563',0),
	('malesuada@yahoo.couk',	'MALESUADA@YAHOO.COUK',	'malesuada@yahoo.couk',	'MALESUADA@YAHOO.COUK',	0,'Calista','Hill','P.O. Box 559, 6066 Lectus Avenue','AQAAAAEAACcQAAAAEDJShDJYCSYiqh9iz2OqMsLydSMKdK7Gipj3OTseNxG+hd06TGAhHrpgmJa2sVTaxQ==','(687) 183-5211',0),
	('hendrerit@hotmail.org', 'HENDRERIT@HOTMAIL.ORG', 'hendrerit@hotmail.org', 'HENDRERIT@HOTMAIL.ORG', 0, 'Salvador','Huffman','986-3229 Urna Street','AQAAAAEAACcQAAAAEKVWi0gVJqXHx69TzKJRfiaC4sH5fq3fNKFf6FXmTLjQfH90l4awh5+FbTwvsFUvVA==','(356) 158-2672',0),
	('donec.tempus@outlook.org','DONEC.TEMPUS@OUTLOOK.ORG','donec.tempus@outlook.org','DONEC.TEMPUS@OUTLOOK.ORG', 0,'Slade','Martin','Ap #278-4822 Nec St.','AQAAAAEAACcQAAAAEJ/JBT6Lb1mYqaKB98HluKt8g+d7H6veX+dyFwOWCX2r7CHep2+HxJ+3t6Oq5lxe+g==','(475) 562-3885',0),
	('mollis.phasellus@outlook.ca','MOLLIS.PHASELLUS@OUTLOOK.CA','mollis.phasellus@outlook.ca','MOLLIS.PHASELLUS@OUTLOOK.CA', 0,'Hollee','Pruitt','991-4470 Scelerisque St.','AQAAAAEAACcQAAAAEJ//l5lmLm0u0Cck7A1+dYU5giSDznt/JH0kakZUmmxDfwZHGMZ6GoKNvYPMMhghmQ==','(644) 232-5558',0),
	('lobortis.tellus@protonmail.org','LOBORTIS.TELLUS@PROTONMAIL.ORG','lobortis.tellus@protonmail.org','LOBORTIS.TELLUS@PROTONMAIL.ORG',0,'Hilda','Snow','389-8048 Dui. Road','AQAAAAEAACcQAAAAEBmjPRmla2LnHzqmaRuKxRrHGWb9hvpkHRNGSmZHVcnS/mVIYhueg1pcaecN2kexZA==','(798) 663-9127',0),
	('nisi.aenean@protonmail.ca','NISI.AENEAN@PROTONMAIL.CA','nisi.aenean@protonmail.ca','NISI.AENEAN@PROTONMAIL.CA', 0, 'Alexandra','Kaufman','P.O. Box 735, 7494 Pellentesque St.','AQAAAAEAACcQAAAAEIZJ6ND8YPjQm4UiKSVimK2ceyaEGRiGghvtlWgmhlGh9tlcYxfvkt02Q9OglOjDsg==','(547) 648-7568',0),
	('est.nunc@yahoo.couk','EST.NUNC@YAHOO.COUK','est.nunc@yahoo.couk','EST.NUNC@YAHOO.COUK',0,'Jillian','Hicks','Ap #571-3597 Pellentesque Rd.','AQAAAAEAACcQAAAAEGKHHbwh2N9Kub5juHrjWzuLcOwH4jOyZTbuxvnMRqxqlYfUkSK8o7LZGevaTV56Fw==','(741) 404-6778',0),
	('quisque@icloud.edu','QUISQUE@ICLOUD.EDU','quisque@icloud.edu','QUISQUE@ICLOUD.EDU',0,'Drew','Henson','P.O. Box 689, 5479 Proin Rd.','AQAAAAEAACcQAAAAEB7Q1tJFb5/Lo5ZqDZ/1wKQHp1iyoxcnxocZJtYO5PW7ENFpBnesCR/G/C/2hX0a3A==','(618) 143-9354',0),
	('a.neque@yahoo.ca','A.NEQUE@YAHOO.CA','a.neque@yahoo.ca','A.NEQUE@YAHOO.CA',0,'Jelani','Franks','Ap #974-9581 Lacus, Street','AQAAAAEAACcQAAAAEDvzSjcekOME+wcu/VuuzfGBbeeFOYyBIZt/yanC6Npz5Ia4vdQzKablnpbBnU0h9w==','(401) 214-5419',0);
GO
-- Checks that table doesn't already exist, then creates it
IF OBJECT_ID(N'Flights.ApplicationRole') IS NULL
BEGIN
	CREATE TABLE Flights.[ApplicationRole]
	(
		[Id] INT NOT NULL PRIMARY KEY IDENTITY,
		[Name] NVARCHAR(256) NOT NULL UNIQUE,
		[NormalizedName] NVARCHAR(256) NOT NULL
	);
END

CREATE INDEX [IX_ApplicationRole_NormalizedName] ON [Flights].[ApplicationRole] ([NormalizedName])

GO
INSERT INTO Flights.[ApplicationRole]([Name], [NormalizedName])
VALUES
	('Admin', 'ADMIN'),
	('Default', 'DEFAULT');
GO
-- Checks that table doesn't already exist, then creates it
IF OBJECT_ID(N'Flights.ApplicationUserRole') IS NULL
BEGIN
	CREATE TABLE Flights.[ApplicationUserRole]
	(
		[UserId] INT NOT NULL,
		[RoleId] INT NOT NULL
		PRIMARY KEY ([UserId], [RoleId]),
		CONSTRAINT [FK_ApplicationUserRole_User] FOREIGN KEY ([UserId]) REFERENCES [Flights].[ApplicationUser]([Id]),
		CONSTRAINT [FK_ApplicationUserRole_Role] FOREIGN KEY ([RoleId]) REFERENCES [Flights].[ApplicationRole]([Id])
	);
END;
WITH CTE AS(
	SELECT 
	U.Id AS 'UserId',
	R.Id AS 'RoleId'
	FROM [Flights].[ApplicationUser] U,[Flights].[ApplicationRole] R
	WHERE U.[UserName] = 'sammcanany@ksu.edu'
	AND R.[Name] = 'Admin'
)
INSERT INTO Flights.[ApplicationUserRole]([RoleId], [UserId])
SELECT * FROM CTE
GO
-- Checks that table doesn't already exist, then creates it
IF OBJECT_ID(N'Flights.TicketInfo') IS NULL
BEGIN
	CREATE TABLE Flights.TicketInfo
	(
		TicketInfoID INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
		ProfileID INT NOT NULL FOREIGN KEY REFERENCES Flights.[ApplicationUser](Id),
		FlightID INT NOT NULL FOREIGN KEY REFERENCES Flights.Flight(FlightID),
		SeatNumber INT NOT NULL

		UNIQUE(FlightID, SeatNumber)
	);
END
INSERT INTO Flights.TicketInfo(ProfileID, FlightID, SeatNumber)
VALUES
	('1', '1', '70'),
	('2', '1', '71'),
	('3', '2', '10'),
	('4', '3', '20'),
	('5', '4', '30'),
	('6', '5', '40'),
	('7', '6', '50'),
	('8', '7', '60'),
	('9', '8', '70'),
	('10', '9', '80');
GO