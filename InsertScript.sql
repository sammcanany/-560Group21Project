
INSERT INTO Flights.Airport(Abbreviation, [Name], AirportCode, [Location])
VALUES
	('ATL','Hartsfield-Jackson International Airport', '1000', 'GA'),
	('DFW','Dallas/Fort Worth International Airport', '2000', 'TX'),
	('DEN','Denver International Airport','3000','CO'),
	('ORD','O''Hare International Airport','4000','IL'),
	('LAX','Los Angeles International Airport','5000','CA'),
	('CLT','Charlotte Douglas International Airport','6000','NC'),
	('LAS','Harry Reid International Airport','7000','NV'),
	('PHX','Phoenix Sky Harbor International Airport','8000', 'AZ'),
	('MCO','Orlando International Airport','9000', 'FL'),
	('MHK','Manhattan Regional Airport','9001', 'KS');

INSERT INTO Flights.Airline([Name], Country)
VALUES
	('Southwest Airlines', 'USA'),
	('American Airlines', 'USA'),
	('Spirit Airlines', 'USA'),
	('Delta Air Lines', 'USA'),
	('United Airlines', 'USA'),
	('Allegiant Air', 'USA');	

	
/*need to add seatstaken and price*/
/*Also need to figure DepartingID and DestinationID*/
INSERT INTO Flights.Flight(FlightNumber, DepartingAirportID, DestinationAirportID, AirlineID, DepartureDate, DepartureTime, ArrivalTime, Capacity, SeatsTaken, Price)
SELECT F.FlightNumber, AP.AirportID, AP.AirportID, AL.AirlineID, F.DepartureDate, F.DepartureTime, F.ArrivalTime, F.Capacity, F.SeatsTaken, F.Price, 
FROM
	(
		VALUES
		/*Need to change the AirlineID*/
		('IGTGD', '1', '2', 'AIRLINE', '2021-10-03','1:52:00','1:52:00', 261, 100, 700),
		('JTRLT', '1', '3', 'AIRLINE', '2023-02-07','23:08:00', '1:52:00', 262, 100, 700),
		('JQOWL', '1', '4', 'AIRLINE', '2021-12-08','12:22:00', '1:52:00', 147, 100, 700),
		('PJVFY', '1', '5', 'AIRLINE', '2022-10-18','10:51:00', '1:52:00', 223, 100, 700),
		('MUNNG', '1', '6', 'AIRLINE', '2023-01-04','18:31:00', '1:52:00', 231, 100, 700),
		('LYMXL', '1', '7', 'AIRLINE', '2022-11-21','11:54:00', '1:52:00', 238, 100, 700),
		('NOBCK', '1', '8', 'AIRLINE', '2021-09-04','8:44:00','1:52:00', 290, 100, 700),
		('LTYWK', '1', '9', 'AIRLINE', '2022-04-17','5:23:00','1:52:00', 135, 100, 700),
		('ZJTBB', '1', '10', 'AIRLINE', '2022-05-13','19:52:00','1:52:00', 252, 100, 700),
		('RJXOW', '2', '1', 'AIRLINE', '2023-03-15','12:28:00','1:52:00', 135, 100, 700),
		('GOWLJ', '2', '3', 'AIRLINE', '2022-10-13','8:16:00','1:52:00', 175, 100, 700),
		('YMLUV', '2', '4', 'AIRLINE', '2022-02-08','9:46:00','1:52:00', 149, 100, 700),
		('HIVVZ', '2', '5', 'AIRLINE', '2023-03-08','8:48:00','1:52:00', 221, 100, 700),
		('UPUME', '2', '6', 'AIRLINE', '2022-05-25','20:26:00','1:52:00', 112, 100, 700),
		('SJFLA', '2', '7', 'AIRLINE', '2021-08-14','17:23:00','1:52:00', 107, 100, 700),
		('SSBSW', '2', '8', 'AIRLINE', '2021-10-18','13:36:00','1:52:00', 277, 100, 700),
		('IETBE', '2', '9', 'AIRLINE', '2022-03-09','19:05:00','1:52:00', 179, 100, 700),
		('VFJBF', '2', '10', 'AIRLINE', '2022-05-10','22:28:00','1:52:00', 273, 100, 700),
		('YSVWE', '3', '1', 'AIRLINE', '2021-10-31','14:38:00','1:52:00', 118, 100, 700),
		('PQJBN', '3', '2', 'AIRLINE', '2023-01-30','17:08:00','1:52:00', 173, 100, 700),
		('LEODQ', '3', '4', 'AIRLINE', '2021-09-16','6:21:00','1:52:00', 225, 100, 700),
		('RWGJK', '3', '5', 'AIRLINE', '2022-05-07','15:28:00','1:52:00', 288, 100, 700),
		('NFXIL', '3', '6', 'AIRLINE', '2022-12-22','7:28:00','1:52:00', 184, 100, 700),
		('WIVCL', '3', '7', 'AIRLINE', '2022-10-29','23:41:00','1:52:00', 235, 100, 700),
		('JJGEV', '3', '8', 'AIRLINE', '2021-09-17','9:57:00','1:52:00', 265, 100, 700),
		('BVKCV', '3', '9', 'AIRLINE', '2022-08-21','22:12:00','1:52:00', 176, 100, 700),
		('STOEW', '3', '10', 'AIRLINE', '2022-05-27','13:59:00','1:52:00', 127, 100, 700),
		('KBKZD', '4', '1', 'AIRLINE', '2023-02-14','17:17:00','1:52:00', 108, 100, 700),
		('HINFI', '4', '2', 'AIRLINE', '2021-08-10','18:55:00','1:52:00', 243, 100, 700),
		('YPNBX', '4', '3', 'AIRLINE', '2022-07-01','17:12:00','1:52:00', 221, 100, 700),
		('GRPYV', '4', '5', 'AIRLINE', '2022-12-06','16:45:00','1:52:00', 250, 100, 700),
		('VRSPT', '4', '6', 'AIRLINE', '2022-04-20','13:35:00','1:52:00', 224, 100, 700),
		('PVXTT', '4', '7', 'AIRLINE', '2021-06-25','17:14:00','1:52:00', 214, 100, 700),
		('ZCFOT', '4', '8', 'AIRLINE', '2022-06-03','20:12:00','1:52:00', 199, 100, 700),
		('UWFQR', '4', '9', 'AIRLINE', '2021-11-08','11:04:00','1:52:00', 102, 100, 700),
		('UNHGA', '4', '10', 'AIRLINE', '2021-04-21','21:45:00','1:52:00', 100, 100, 700),
		('GVFFF', '5', '1', 'AIRLINE', '2021-09-02','17:46:00','1:52:00', 283, 100, 700),
		('XSCYE', '5', '2', 'AIRLINE', '2022-05-19','20:03:00','1:52:00', 157, 100, 700),
		('PGMGN', '5', '3', 'AIRLINE', '2022-05-28','16:54:00','1:52:00', 266, 100, 700),
		('KJRXM', '5', '4', 'AIRLINE', '2021-12-04','2:23:00','1:52:00', 233, 100, 700),
		('EEIGU', '5', '6', 'AIRLINE', '2022-04-13','19:57:00','1:52:00', 189, 100, 700),
		('RUIXT', '5', '7', 'AIRLINE', '2022-01-10','19:52:00','1:52:00', 106, 100, 700),
		('IVLTH', '5', '8', 'AIRLINE', '2023-01-11','16:56:00','1:52:00', 281, 100, 700),
		('LQGNA', '5', '9', 'AIRLINE', '2021-10-05','6:09:00','1:52:00', 181, 100, 700),
		('UYBYM', '5', '10', 'AIRLINE', '2021-09-04','7:54:00','1:52:00', 132, 100, 700),
		('JQMYU', '6', '1', 'AIRLINE', '2022-10-29','20:52:00','1:52:00', 273, 100, 700),
		('TAIJW', '6', '2', 'AIRLINE', '2021-09-29','19:22:00','1:52:00', 252, 100, 700),
		('SDOYQ', '6', '3', 'AIRLINE', '2023-04-06','5:41:00','1:52:00', 247, 100, 700),
		('ILZGR', '6', '4', 'AIRLINE', '2023-03-03','0:26:00','1:52:00', 197, 100, 700),
		('PNSJM', '6', '5', 'AIRLINE', '2022-08-31','8:24:00','1:52:00', 128, 100, 700)
	) F(FlightNumber, DepartingAirportID, DestinationAirportID, AirlineID, DepartureDate, DepartureTime, ArrivalTime, Capacity)
	INNER JOIN Flights.Airport AP ON AP.AirportID = F.DepartingAirportID
	 AND  AP.AirportID = F.DestinationAirportID
	INNER JOIN Flights.Airline AL ON AL.AirlineID = F.AirlineID


INSERT INTO Flights.TicketInfo(FlightID, SeatNumber)
SELECT F.FlightID, T.SeatNumber
FROM
	(
		VALUES
			('1', '70'),
			('1', '71'),
			('2', '10'),
			('3', '20'),
			('4', '30'),
			('5', '40'),
			('6', '50'),
			('7', '60'),
			('8', '70'),
			('9', '80')
	)T(FlightID, SeatNumber)
	INNER JOIN Flights.Flight F ON F.FlightID = T.FlightID

/*Not finished*/
INSERT INTO Flights.ApplicationUser(UserName, NormalizedUserName, Email, NormalizedEmail, EmailConfirmed, FirstName, LastName, [Address], PasswordHash, PhoneNumber, PhoneNumberConfirmed)
VALUES
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


INSERT INTO Flights.ApplicationRole([Name], NormalizeName)
VALUES
	()

INSERT INTO Flights.ApplicationUserRole()
VALUES
