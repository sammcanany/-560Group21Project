
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


INSERT INTO Flights.Airline([Name], Country)
VALUES
	('Southwest Airlines', 'United States Of America'),
	('American Airlines', 'United States Of America'),
	('Spirit Airlines', 'United States Of America'),
	('Delta Air Lines', 'United States Of America'),
	('United Airlines', 'United States Of America'),
	('Allegiant Air', 'United States Of America');	
	

INSERT INTO Flights.Flight(FlightNumber, DepartingAirportID, DestinationAirportID, AirlineID, DepartureDate, DepartureTime, ArrivalTime, Capacity, SeatsTaken, Price)
SELECT F.FlightNumber, AP.AirportID, AP.AirportID, AL.AirlineID, F.DepartureDate, F.DepartureTime, F.ArrivalTime, F.Capacity, F.SeatsTaken, F.Price
FROM
	(
		VALUES
			('IGTGD', '1', '2', 1, '2021-10-03','12:00:00','1:52:00',261,0,125),
			('JTRLT', '1', '3', 1, '2023-02-07','12:00:00','23:08:00',262,0,125),
			('JQOWL', '1', '4', 2, '2021-12-08','12:00:00','12:22:00',147,0,125),
			('PJVFY', '1', '5', 2, '2022-10-18','12:00:00','10:51:00',223,0,125),
			('MUNNG', '1', '6', 3, '2023-01-04','12:00:00','18:31:00',231,0,125),
			('LYMXL', '1', '7', 4, '2022-11-21','12:00:00','11:54:00',238,0,125),
			('NOBCK', '1', '8', 4, '2021-09-04','12:00:00','8:44:00',290,0,125),
			('LTYWK', '1', '9', 5, '2022-04-17','12:00:00','5:23:00',135,0,125),
			('ZJTBB', '1', '10', 6, '2022-05-13','12:00:00','19:52:00',252,0,125)
	) F(FlightNumber, DepartingAirportID, DestinationAirportID, AirlineID, DepartureDate, DepartureTime, ArrivalTime, Capacity, SeatsTaken, Price)
	INNER JOIN Flights.Airport AP ON AP.AirportID = F.DepartingAirportID
	 /*AND  AP.AirportID = F.DestinationAirportID*/
	INNER JOIN Flights.Airline AL ON AL.AirlineID = F.AirlineID



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


INSERT INTO Flights.TicketInfo(ProfileID, FlightID, SeatNumber)
SELECT AU.Id, F.FlightID, T.SeatNumber 
FROM
	(
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
			('10', '9', '80')
	)T(ProfileID, FlightID, SeatNumber)
	INNER JOIN Flights.Flight F ON F.FlightID = T.FlightID
	INNER JOIN Flights.ApplicationUser AU ON AU.Id = T.ProfileID