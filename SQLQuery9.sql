﻿ALTER TABLE DemoFacility
DROP COLUMN Facility_no;

ALTER TABLE DemoFacility
ADD Facility_no INT PRIMARY KEY IDENTITY(1,1);
