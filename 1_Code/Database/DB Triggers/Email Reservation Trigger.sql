USE [HotelTango]
GO

/****** Object:  Trigger [dbo].[EmailReservation]    Script Date: 11/27/2022 3:19:32 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





CREATE      TRIGGER [dbo].[EmailReservation]
ON [dbo].[Reservation]
AFTER INSERT
AS
	DECLARE
	 @Customer_Name varchar(500)
	,@RoomTypeName varchar(500)
	,@RoomNumber int
	,@WIFI_Passcode varchar(500)
	,@StartDate date
	,@EndDate date
	,@EmailBody varchar(max)
	,@EmailSubject varchar(max)

	SELECT TOP 1
		 @Customer_Name = c.FirstName +' '+c.LastName
		,@RoomTypeName = rt.RoomTypeName
		,@RoomNumber = ro.RoomNumber
		,@WIFI_Passcode = res.[WIFI_Passcode]
		,@StartDate = res.[StartDate]
		,@EndDate = res.[EndDate]
	FROM 
		Reservation res
	INNER JOIN
		Customer c 
	ON
		res.CustomerID = c.Id
	INNER JOIN 
		Room ro
	ON 
	  res.RoomID = ro.Id
	INNER JOIN 
		RoomType rt
	ON
		ro.RoomTypeID = rt.Id
	WHERE
		res.Id = 
	(
		SELECT 
			MAX(Id) 
		FROM 
			Reservation
	)

  SET @EmailSubject = 'New Reservation Created For - '+@Customer_Name

  SET @EmailBody = 'New Reservation Alert'
  SET @EmailBody = @EmailBody + CHAR(10)+CHAR(13)
  SET @EmailBody = @EmailBody + 'Customer Name: '+@Customer_Name 
  SET @EmailBody = @EmailBody + CHAR(10)+CHAR(13)
  SET @EmailBody = @EmailBody + 'RoomTypeName: '+@RoomTypeName 
  SET @EmailBody = @EmailBody + CHAR(10)+CHAR(13)
  SET @EmailBody = @EmailBody + 'Room Number: '+CAST(@RoomNumber AS varchar(500)) 
  SET @EmailBody = @EmailBody + CHAR(10)+CHAR(13)
  SET @EmailBody = @EmailBody + 'WIFI Passcode: '+@WIFI_Passcode 
  SET @EmailBody = @EmailBody + CHAR(10)+CHAR(13)
  SET @EmailBody = @EmailBody + 'Start Date: '+CAST(@StartDate AS varchar(500)) 
  SET @EmailBody = @EmailBody + CHAR(10)+CHAR(13)
  SET @EmailBody = @EmailBody + 'End Date: '+CAST(@EndDate AS varchar(500)) 
  SET @EmailBody = @EmailBody + CHAR(10)+CHAR(13)

	EXEC msdb.dbo.sp_send_dbmail
	  @profile_name = 'default',
	  @recipients = 'sstepter@gmail.com',
	  @body = @EmailBody,
	  @subject = @EmailSubject
GO

ALTER TABLE [dbo].[Reservation] ENABLE TRIGGER [EmailReservation]
GO


