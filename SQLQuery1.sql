﻿CREATE TABLE DemoFacility ( Facility_no int NOT NULL PRIMARY KEY, Name VARCHAR(30) NOT NULL, Hotel_no int NOT NULL, FOREIGN KEY (Hotel_no) REFERENCES DemoHotel(Hotel_no))