﻿CREATE PROCEDURE [dbo].[AddPlayer]
	@FIRSTNAME NVARCHAR(50),
	@LASTNAME NVARCHAR(50),
	@SEX NVARCHAR(20),
	@AGE int,
	@STATE NVARCHAR(20),
	@EMAIL NVARCHAR(50),
	@USERNAME NVARCHAR(50),
	@PASSWORD NVARCHAR(50)

AS
	BEGIN
	Insert into Player(FIRSTNAME, LASTNAME, SEX, AGE, STATE, EMAIL, USERNAME, PASSWORD)
	values(@FIRSTNAME, @LASTNAME, @SEX, @AGE, @STATE, @EMAIL, @USERNAME, @PASSWORD)
	end
RETURN 0
