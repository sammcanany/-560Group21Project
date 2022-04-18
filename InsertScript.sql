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

INSERT INTO Flights.Airline(AirlineID, Name, Country)
VALUES
	

INSERT INTO Flights.Flight(FlightID, FlightNumber, DepartingAirportID, DestinationAirportID, AirlineID, DepartureDate, DepartureTime, ArrivalTime, Capacity)
VALUES
	-- missing airlineID and arrival time
	(1, 'IGTGD', '1', '2', 'AIRLINE', '2021-10-03','1:52:00',261),
	(2, 'JTRLT', '1', '3', 'AIRLINE', '2023-02-07','23:08:00',262),
	(3, 'JQOWL', '1', '4', 'AIRLINE', '2021-12-08','12:22:00',147),
	(4, 'PJVFY', '1', '5', 'AIRLINE', '2022-10-18','10:51:00',223),
	(5, 'MUNNG', '1', '6', 'AIRLINE', '2023-01-04','18:31:00',231),
	(6, 'LYMXL', '1', '7', 'AIRLINE', '2022-11-21','11:54:00',238),
	(7, 'NOBCK', '1', '8', 'AIRLINE', '2021-09-04','8:44:00',290),
	(8, 'LTYWK', '1', '9', 'AIRLINE', '2022-04-17','5:23:00',135),
	(9, 'ZJTBB', '1', '10', 'AIRLINE', '2022-05-13','19:52:00',252),
	(10, 'RJXOW', '2', '1', 'AIRLINE', '2023-03-15','12:28:00',135),
	(11, 'GOWLJ', '2', '3', 'AIRLINE', '2022-10-13','8:16:00',175),
	(12, 'YMLUV', '2', '4', 'AIRLINE', '2022-02-08','9:46:00',149),
	(13, 'HIVVZ', '2', '5', 'AIRLINE', '2023-03-08','8:48:00',221),
	(14, 'UPUME', '2', '6', 'AIRLINE', '2022-05-25','20:26:00',112),
	(15, 'SJFLA', '2', '7', 'AIRLINE', '2021-08-14','17:23:00',107),
	(16, 'SSBSW', '2', '8', 'AIRLINE', '2021-10-18','13:36:00',277),
	(17, 'IETBE', '2', '9', 'AIRLINE', '2022-03-09','19:05:00',179),
	(18, 'VFJBF', '2', '10', 'AIRLINE', '2022-05-10','22:28:00',273),
	(19, 'YSVWE', '3', '1', 'AIRLINE', '2021-10-31','14:38:00',118),
	(20, 'PQJBN', '3', '2', 'AIRLINE', '2023-01-30','17:08:00',173),
	(21, 'LEODQ', '3', '4', 'AIRLINE', '2021-09-16','6:21:00',225),
	(22, 'RWGJK', '3', '5', 'AIRLINE', '2022-05-07','15:28:00',288),
	(23, 'NFXIL', '3', '6', 'AIRLINE', '2022-12-22','7:28:00',184),
	(24, 'WIVCL', '3', '7', 'AIRLINE', '2022-10-29','23:41:00',235),
	(25, 'JJGEV', '3', '8', 'AIRLINE', '2021-09-17','9:57:00',265),
	(26, 'BVKCV', '3', '9', 'AIRLINE', '2022-08-21','22:12:00',176),
	(27, 'STOEW', '3', '10', 'AIRLINE', '2022-05-27','13:59:00',127),
	(28, 'KBKZD', '4', '1', 'AIRLINE', '2023-02-14','17:17:00',108),
	(29, 'HINFI', '4', '2', 'AIRLINE', '2021-08-10','18:55:00',243),
	(30, 'YPNBX', '4', '3', 'AIRLINE', '2022-07-01','17:12:00',221),
	(31, 'GRPYV', '4', '5', 'AIRLINE', '2022-12-06','16:45:00',250),
	(32, 'VRSPT', '4', '6', 'AIRLINE', '2022-04-20','13:35:00',224),
	(33, 'PVXTT', '4', '7', 'AIRLINE', '2021-06-25','17:14:00',214),
	(34, 'ZCFOT', '4', '8', 'AIRLINE', '2022-06-03','20:12:00',199),
	(35, 'UWFQR', '4', '9', 'AIRLINE', '2021-11-08','11:04:00',102),
	(36, 'UNHGA', '4', '10', 'AIRLINE', '2021-04-21','21:45:00',100),
	(37, 'GVFFF', '5', '1', 'AIRLINE', '2021-09-02','17:46:00',283),
	(38, 'XSCYE', '5', '2', 'AIRLINE', '2022-05-19','20:03:00',157),
	(39, 'PGMGN', '5', '3', 'AIRLINE', '2022-05-28','16:54:00',266),
	(40, 'KJRXM', '5', '4', 'AIRLINE', '2021-12-04','2:23:00',233),
	(41, 'EEIGU', '5', '6', 'AIRLINE', '2022-04-13','19:57:00',189),
	(42, 'RUIXT', '5', '7', 'AIRLINE', '2022-01-10','19:52:00',106),
	(43, 'IVLTH', '5', '8', 'AIRLINE', '2023-01-11','16:56:00',281),
	(44, 'LQGNA', '5', '9', 'AIRLINE', '2021-10-05','6:09:00',181),
	(45, 'UYBYM', '5', '10', 'AIRLINE', '2021-09-04','7:54:00',132),
	(46, 'JQMYU', '6', '1', 'AIRLINE', '2022-10-29','20:52:00',273),
	(47, 'TAIJW', '6', '2', 'AIRLINE', '2021-09-29','19:22:00',252),
	(48, 'SDOYQ', '6', '3', 'AIRLINE', '2023-04-06','5:41:00',247),
	(49, 'ILZGR', '6', '4', 'AIRLINE', '2023-03-03','0:26:00',197),
	(50, 'PNSJM', '6', '5', 'AIRLINE', '2022-08-31','8:24:00',128);

INSERT INTO Flights.Class(ClassID, Name)
VALUES

INSERT INTO Flights.FlightClass(FLightID, ClassID, Price)
VALUES

INSERT INTO Flights.PassengerProfile(ProfileID, FirstName, LastName, EmailAdress, [Address], [Password], PhoneNumber)
VALUES

INSERT INTO Flights.TicketInfo(TicketInfoID, ProfileID, FlightID, ClassID, SeatNumber)
VALUES