IF OBJECT_ID(N'Audit.Audit_Customer', N'U') IS NULL
create table [Audit].Audit_Customer
(
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](max) NULL,
	[LastName] [nvarchar](max) NULL,
	[Address] [nvarchar](max) NULL,
	[City] [nvarchar](max) NULL,
	[State] [nvarchar](max) NULL,
	[PostalCode] [int] NOT NULL,
	[EmailAddress] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[Action] nvarchar(max) null,
	UpdatedBy nvarchar(max) null,
	UpdatedOn datetime2
)
GO
CREATE OR ALTER TRIGGER Customer_Insert
ON Customer
FOR INSERT
AS
   INSERT INTO [Audit].Audit_Customer
   (
		[Id],
		[FirstName],
		[LastName],
		[Address],
		[City],
		[State],
		[PostalCode],
		[EmailAddress],
		[PhoneNumber],
		[Action],
		UpdatedBy,
		UpdatedOn
   )
   SELECT 	
	[Id],
	[FirstName],
	[LastName],
	[Address],
	[City],
	[State],
	[PostalCode],
	[EmailAddress],
	[PhoneNumber],
	'INSERT', 
	GETDATE(), 
	SUSER_NAME()
   FROM 
	INSERTED;
GO

CREATE OR ALTER TRIGGER Customer_Update
ON Customer
FOR Update
AS
   INSERT INTO [Audit].Audit_Customer
   (
		[Id],
		[FirstName],
		[LastName],
		[Address],
		[City],
		[State],
		[PostalCode],
		[EmailAddress],
		[PhoneNumber],
		[Action],
		UpdatedBy,
		UpdatedOn
   )
   SELECT 	
	[Id],
	[FirstName],
	[LastName],
	[Address],
	[City],
	[State],
	[PostalCode],
	[EmailAddress],
	[PhoneNumber],
	'UPDATE', 
	GETDATE(), 
	SUSER_NAME()
   FROM 
	inserted;
GO


CREATE OR ALTER TRIGGER Customer_Insert
ON Customer
FOR INSERT
AS
   INSERT INTO [Audit].Audit_Customer
   (
		[Id],
		[FirstName],
		[LastName],
		[Address],
		[City],
		[State],
		[PostalCode],
		[EmailAddress],
		[PhoneNumber],
		[Action],
		UpdatedBy,
		UpdatedOn
   )
   SELECT 	
	[Id],
	[FirstName],
	[LastName],
	[Address],
	[City],
	[State],
	[PostalCode],
	[EmailAddress],
	[PhoneNumber],
	'INSERT', 
	GETDATE(), 
	SUSER_NAME()
   FROM 
	INSERTED;
GO


CREATE OR ALTER TRIGGER Customer_Delete
ON Customer
FOR DELETE
AS
   INSERT INTO [Audit].Audit_Customer
   (
		[Id],
		[FirstName],
		[LastName],
		[Address],
		[City],
		[State],
		[PostalCode],
		[EmailAddress],
		[PhoneNumber],
		[Action],
		UpdatedBy,
		UpdatedOn
   )
   SELECT 	
	[Id],
	[FirstName],
	[LastName],
	[Address],
	[City],
	[State],
	[PostalCode],
	[EmailAddress],
	[PhoneNumber],
	'Delete', 
	GETDATE(), 
	SUSER_NAME()
   FROM 
	deleted;
GO

IF OBJECT_ID(N'Audit.Audit_Reservation', N'U') IS NULL
CREATE TABLE [Audit].[Audit_Reservation]
(
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CustomerID] [int] NOT NULL,
	[RoomID] [int] NOT NULL,
	[WIFI_Passcode] [nvarchar](max) NULL,
	[StartDate] [datetime2](7) NOT NULL,
	[EndDate] [datetime2](7) NOT NULL,
	[Action] nvarchar(max) null,
	UpdatedBy nvarchar(max) null,
	UpdatedOn datetime2
)
GO

CREATE OR ALTER TRIGGER Reservation_Insert
ON Reservation
FOR INSERT
AS
   INSERT INTO [Audit].Audit_Reservation
   (
		[Id],
		[CustomerID],
		[RoomID],
		[WIFI_Passcode],
		[StartDate],
		[EndDate],
		[Action],
		UpdatedBy,
		UpdatedOn
   )
   SELECT 	
		[Id],
		[CustomerID],
		[RoomID],
		[WIFI_Passcode],
		[StartDate],
		[EndDate],
		'INSERT', 
		GETDATE(), 
		SUSER_NAME()
	FROM 
		INSERTED;
GO


CREATE OR ALTER TRIGGER Reservation_Update
ON Reservation
FOR UPDATE
AS
   INSERT INTO [Audit].Audit_Reservation
   (
		[Id],
		[CustomerID],
		[RoomID],
		[WIFI_Passcode],
		[StartDate],
		[EndDate],
		[Action],
		UpdatedBy,
		UpdatedOn
   )
   SELECT 	
		[Id],
		[CustomerID],
		[RoomID],
		[WIFI_Passcode],
		[StartDate],
		[EndDate],
		'UPDATE', 
		GETDATE(), 
		SUSER_NAME()
	FROM 
		INSERTED;
GO


CREATE OR ALTER TRIGGER Reservation_Update
ON Reservation
FOR DELETE
AS
   INSERT INTO [Audit].Audit_Reservation
   (
		[Id],
		[CustomerID],
		[RoomID],
		[WIFI_Passcode],
		[StartDate],
		[EndDate],
		[Action],
		UpdatedBy,
		UpdatedOn
   )
   SELECT 	
		[Id],
		[CustomerID],
		[RoomID],
		[WIFI_Passcode],
		[StartDate],
		[EndDate],
		'DELETE', 
		GETDATE(), 
		SUSER_NAME()
	FROM 
		deleted;
GO

IF OBJECT_ID(N'Audit.Audit_Room', N'U') IS NULL
CREATE TABLE [Audit].[Audit_Room]
(
	[Id] [int] NOT NULL,
	[RoomNumber] [int] NOT NULL,
	[RoomTypeID] [int] NOT NULL,
	[Action] nvarchar(max) null,
	UpdatedBy nvarchar(max) null,
	UpdatedOn datetime2
)

GO

CREATE OR ALTER TRIGGER Room_Insert
ON Room
FOR INSERT
AS
   INSERT INTO [Audit].Audit_Room
   (
		[Id],
		[RoomNumber],
		[RoomTypeID],
		[Action],
		UpdatedBy,
		UpdatedOn
   )
   SELECT 	
		[Id],
		[RoomNumber],
		[RoomTypeID],
		'INSERT', 
		GETDATE(), 
		SUSER_NAME()
	FROM 
		INSERTED;
GO

CREATE OR ALTER TRIGGER Room_Update
ON Room
FOR UPDATE
AS
   INSERT INTO [Audit].Audit_Room
   (
		[Id],
		[RoomNumber],
		[RoomTypeID],
		[Action],
		UpdatedBy,
		UpdatedOn
   )
   SELECT 	
		[Id],
		[RoomNumber],
		[RoomTypeID],
		'UPDATE', 
		GETDATE(), 
		SUSER_NAME()
	FROM 
		INSERTED;
GO

CREATE OR ALTER TRIGGER Room_Delete
ON Room
FOR DELETE
AS
   INSERT INTO [Audit].Audit_Room
   (
		[Id],
		[RoomNumber],
		[RoomTypeID],
		[Action],
		UpdatedBy,
		UpdatedOn
   )
   SELECT 	
		[Id],
		[RoomNumber],
		[RoomTypeID],
		'DELETE', 
		GETDATE(), 
		SUSER_NAME()
	FROM 
		DELETED;
GO

IF OBJECT_ID(N'Audit.Audit_RoomType', N'U') IS NULL
CREATE TABLE [Audit].[Audit_RoomType]
(
	[Id] [int] NOT NULL,
	[RoomTypeName] [nvarchar](max) NULL,
	[BedType] [nvarchar](max) NULL,
	[NumberOfBeds] [int] NOT NULL,
	[RoomRate] [int] NOT NULL,
	[Action] nvarchar(max) null,
	UpdatedBy nvarchar(max) null,
	UpdatedOn datetime2
)
GO


CREATE OR ALTER TRIGGER RoomType_Insert
ON RoomType
FOR INSERT
AS
   INSERT INTO [Audit].Audit_RoomType
   (
		[Id],
		[RoomTypeName],
		[BedType],
		[NumberOfBeds],
		[RoomRate],
		[Action],
		UpdatedBy,
		UpdatedOn
   )
   SELECT 	
		[Id],
		[RoomTypeName],
		[BedType],
		[NumberOfBeds],
		[RoomRate],
		'INSERT', 
		GETDATE(), 
		SUSER_NAME()
	FROM 
		INSERTED;
GO

CREATE OR ALTER TRIGGER RoomType_Update
ON RoomType
FOR UPDATE
AS
   INSERT INTO [Audit].Audit_RoomType
   (
		[Id],
		[RoomTypeName],
		[BedType],
		[NumberOfBeds],
		[RoomRate],
		[Action],
		UpdatedBy,
		UpdatedOn
   )
   SELECT 	
		[Id],
		[RoomTypeName],
		[BedType],
		[NumberOfBeds],
		[RoomRate],
		'UPDATE', 
		GETDATE(), 
		SUSER_NAME()
	FROM 
		inserted;
GO

CREATE OR ALTER TRIGGER RoomType_Delete
ON RoomType
FOR DELETE
AS
   INSERT INTO [Audit].Audit_RoomType
   (
		[Id],
		[RoomTypeName],
		[BedType],
		[NumberOfBeds],
		[RoomRate],
		[Action],
		UpdatedBy,
		UpdatedOn
   )
   SELECT 	
		[Id],
		[RoomTypeName],
		[BedType],
		[NumberOfBeds],
		[RoomRate],
		'DELETE', 
		GETDATE(), 
		SUSER_NAME()
	FROM 
		deleted;
GO