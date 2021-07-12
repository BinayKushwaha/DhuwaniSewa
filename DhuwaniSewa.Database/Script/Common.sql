----------------2021/07/11--------------------------------
INSERT INTO Category(Enum,DisplayName,Description)
VALUES('VehicleBrand','Vehicle Brand','Details vehicle brand.');

INSERT INTO Category(Enum,DisplayName,Description)
VALUES('VehicleType','Vehicle Type','Details vehicle type.');

DECLARE @CategoryId int=(SELECT TOP 1 Id FROM Category WHERE Enum='VehicleBrand')

INSERT INTO Choice(Enum,DisplayName,CategoryId)VALUES('Tata','TATA',@CategoryId);
INSERT INTO Choice(Enum,DisplayName,CategoryId)VALUES('AshokLeyland','Ashok Leyland',@CategoryId);

DECLARE @CategoryId int=(SELECT TOP 1 Id FROM Category WHERE Enum='VehicleType')

INSERT INTO Choice(Enum,DisplayName,CategoryId)VALUES('Truck','Truck',@CategoryId);
INSERT INTO Choice(Enum,DisplayName,CategoryId)VALUES('MiniTruck','Mini Truck',@CategoryId);
---------------------------------END-------------------------------------------------------