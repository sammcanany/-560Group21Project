IF EXISTS(SELECT 1 FROM sys.tables WHERE object_id = OBJECT_ID('Airport'))
BEGIN;
    DROP TABLE [Airport];
END;
GO

CREATE TABLE Airport (
	AirportCode CHAR(3) NOT NULL PRIMARY KEY,
	AirportName VARCHAR(50) NOT NULL,
	AirportState CHAR(2) NOT NULL,
	Longitude FLOAT NOT NULL,
	Latitude FLOAT NOT NULL
	);
GO

INSERT INTO [Airport] (AirportCode,AirportName,AirportState,Longitude,Latitude)
VALUES
('ATL','Hartsfield-Jackson International Airport','GA', 33.640411,-84.419853),
('DFW','Dallas/Fort Worth International Airport','TX', 32.897480,-97.040443),
('DEN','Denver International Airport','CO', 39.849312,-104.673828),
('ORD','O''Hare International Airport','IL', 41.978611,-87.904724),
('LAX','Los Angeles International Airport','CA', 33.942791,-118.410042),
('CLT','Charlotte Douglas International Airport','NC', 35.213890,-80.943054),
('LAS','Harry Reid International Airport','NV', 36.086010,-115.153969),
('PHX','Phoenix Sky Harbor International Airport','AZ', 33.437269,-112.007788),
('MCO','Orlando International Airport','FL', 28.431881,-81.308304),
('MHK','Manhattan Regional Airport','KS', 39.141222,-96.671806);
