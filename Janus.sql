/****************************************************************/
-- Script: janus.sql
-- Author: Matthew Martin 000338807
-- Date: September 18th, 2017
-- Description: Databse for Janus Capstone Project
/****************************************************************/

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
USE [Janus];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO




-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

--- Absence Claims

IF (OBJECT_ID('FK_AbsenceClaims_userID', 'F') IS NOT NULL)
BEGIN
    ALTER TABLE [dbo].[AbsenceClaims] DROP CONSTRAINT FK_AbsenceClaims_userID
END

--Availibility Table

IF (OBJECT_ID('FK_Availibility_userID', 'F') IS NOT NULL)
BEGIN
    ALTER TABLE [dbo].[Availibility] DROP CONSTRAINT FK_Availibility_userID
END

-- Departments Table

IF (OBJECT_ID('FK_Departments_managerID', 'F') IS NOT NULL)
BEGIN
    ALTER TABLE [dbo].[Departments] DROP CONSTRAINT FK_Departments_managerID
END

-- Managers Table

IF (OBJECT_ID('FK_Managers_managerID', 'F') IS NOT NULL)
BEGIN
    ALTER TABLE [dbo].[Managers] DROP CONSTRAINT FK_Managers_userID
END

-- Managers Table

IF (OBJECT_ID('FK_Messages_MailFromUserID', 'F') IS NOT NULL)
BEGIN
   ALTER TABLE [dbo].[Messages] DROP CONSTRAINT [FK_Messages_MailFromUserID]
END

IF (OBJECT_ID('FK_Messages_MailToUserID', 'F') IS NOT NULL)
BEGIN
   ALTER TABLE [dbo].[Messages] DROP CONSTRAINT [FK_Messages_MailToUserID]
END

-- Recoveries

IF (OBJECT_ID('FK_Recoveries_userID', 'F') IS NOT NULL)
BEGIN
    ALTER TABLE [dbo].[Recoveries] DROP CONSTRAINT FK_Recoveries_userID
END

--Roles

IF (OBJECT_ID('FK_Roles_userID', 'F') IS NOT NULL)
BEGIN
    ALTER TABLE [dbo].[Roles] DROP CONSTRAINT FK_Roles_userID
END

-- shiftRequests Table

IF (OBJECT_ID('FK_shiftRequests_managerID', 'F') IS NOT NULL)
BEGIN
    ALTER TABLE [dbo].[shiftRequests] DROP CONSTRAINT FK_shiftRequests_managerID
END


-- Shifts

IF (OBJECT_ID('FK_Shifts_userID', 'F') IS NOT NULL)
BEGIN
    ALTER TABLE [dbo].[Shifts] DROP CONSTRAINT FK_Shifts_userID
END


--Users
IF (OBJECT_ID('FK_Users_companyID', 'F') IS NOT NULL)
BEGIN
    ALTER TABLE [dbo].[Users] DROP CONSTRAINT FK_Users_companyID
END

IF (OBJECT_ID('FK_Users_companyID', 'F') IS NOT NULL)
BEGIN
    ALTER TABLE [dbo].[Users] DROP CONSTRAINT FK_Users_companyID
END

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID('dbo.AbsenceClaims', 'U') IS NOT NULL 
  DROP TABLE dbo.AbsenceClaims; 


IF OBJECT_ID('dbo.Availibility', 'U') IS NOT NULL 
  DROP TABLE dbo.Availibility; 


IF OBJECT_ID('dbo.[Messages]', 'U') IS NOT NULL 
  DROP TABLE dbo.[Messages]; 

IF OBJECT_ID('dbo.Roles', 'U') IS NOT NULL 
  DROP TABLE dbo.Roles; 

IF OBJECT_ID('dbo.shiftRequests', 'U') IS NOT NULL 
  DROP TABLE dbo.shiftRequests; 

IF OBJECT_ID('dbo.Shifts', 'U') IS NOT NULL 
  DROP TABLE dbo.Shifts; 

IF OBJECT_ID('dbo.Users', 'U') IS NOT NULL 
  DROP TABLE dbo.Users; 


-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Messages'
CREATE TABLE [dbo].[Messages] (
    [messageID] int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[mailFromUserID] int  NOT NULL,
	[mailFromUsername] nvarchar(max) NOT NULL,

    [mailToUserID] int  NOT NULL,
	[mailToUsername] nvarchar(max) NOT NULL,
    [subject] nvarchar(max)  NOT NULL,
    [body] nvarchar(max)  NOT NULL,
	shiftRequestID int NULL,
	isRead bit NOT NULL

);
GO


-- Creating table 'Availibilities'
CREATE TABLE [dbo].[Availibility] (
    [availibilityID] int IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [userID] int  NOT NULL,
    [startTime] int NULL,
    [endTime]int NULL,
    [day] nvarchar(max)  NOT NULL
);
GO


-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    userID int IDENTITY(1,1) NOT NULL PRIMARY KEY,
    firstName nvarchar(max)  NOT NULL,
    lastName nvarchar(max)  NOT NULL,
    birthDate nvarchar(max)  NOT NULL,
    [password] nvarchar(max)  NOT NULL,
    [phone] nvarchar(max)  NOT NULL,
    [email] nvarchar(max)  NOT NULL,
	[streetAddress] nvarchar(max)  NOT NULL,
    [postalCode] nvarchar(max)  NOT NULL,
	[role] nvarchar(max)  NOT NULL,
	departmentName nvarchar(max)  NOT NULL,
    hireDate datetime  NOT NULL,
    fireDate datetime  NULL,
    employmentStatus nvarchar(max)  NOT NULL,
	[question] nvarchar(max)  NOT NULL,
    [userAnswer] nvarchar(max)  NOT NULL
);
GO


-- Creating table 'Shifts'
CREATE TABLE [dbo].[Shifts] (
    [shiftID] int IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [userID] int  NOT NULL,
	[shiftDate]  nvarchar(max) NOT NULL,
    [shiftStart] int  NOT NULL,
    [shiftEnd] int  NOT NULL,
    [position] nvarchar(max)  NOT NULL,
    [description] nvarchar(max)  NOT NULL,
    [status] nvarchar(max)  NOT NULL
);
GO




-- Creating table 'AbsenceClaims'
CREATE TABLE [dbo].[AbsenceClaims] (
    [claimID] int IDENTITY(1,1) NOT NULL PRIMARY KEY,
    userID int  NOT NULL,
    [startTime] nvarchar(max)  NOT NULL,
    [endTime] nvarchar(max)  NOT NULL,
    [description] nvarchar(max)  NOT NULL,
    [claimType] nvarchar(max)  NOT NULL,
    [isApproved] bit  NULL
);
GO

-- Creating table 'shiftRequests'
CREATE TABLE [dbo].[shiftRequests] (
    [shiftRequestID] int IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [managerSignOff] nvarchar(max)  NULL,
    [requestor] nvarchar(max)  NOT NULL,
	[requestorID] int  NOT NULL,
	[requestorShiftID] int  NOT NULL,
	[requestorShift] nvarchar(max)  NOT NULL,
    [requestWith] nvarchar(max)  NOT NULL,
	[requestWithShiftID] int  NOT NULL,
	[requestWithShift] nvarchar(max)  NOT NULL,
	[requestWithID] int NOT NULL,
    [requestConfirmed] bit  NULL,
    [requestStatus] nvarchar(max)  NOT NULL
);
GO






-----------------------------------------------------
-- ADD FOREIGN KEY CONSTRAINTS
-----------------------------------------------------


--Users Table


-- Absence Claims Table
ALTER TABLE [dbo].[AbsenceClaims]
ADD CONSTRAINT [FK_AbsenceClaims_userID] FOREIGN KEY (userID)
REFERENCES Users(userID);

--Availibility Table

ALTER TABLE [dbo].[Availibility]
ADD CONSTRAINT [FK_Availibility_userID] FOREIGN KEY (userID)
REFERENCES Users(userID);
--Messages

ALTER TABLE [dbo].[Messages]
ADD CONSTRAINT [FK_Messages_MailToUserID] FOREIGN KEY (mailToUserID)
REFERENCES Users(userID);

ALTER TABLE [dbo].[Messages]
ADD CONSTRAINT [FK_Messages_MailFromUserID] FOREIGN KEY (mailFromUserID)
REFERENCES Users(userID);

--Shifts Table

ALTER TABLE [dbo].[Shifts]
ADD CONSTRAINT [FK_Shifts_userID] FOREIGN KEY (userID)
REFERENCES Users(userID);


-- --------------------------------------------------
-- Sample Data Population
-- --------------------------------------------------


--Insert a weeks worth of shifts for every employee
INSERT INTO Shifts VALUES('2','2018-02-04','9','17','Manager','Supervise Employees in your department','Assigned')
INSERT INTO Shifts VALUES('2','2018-02-05','9','17','Manager','Supervise Employees in your department','Assigned')
INSERT INTO Shifts VALUES('2','2018-02-06','9','17','Manager','Supervise Employees in your department','Assigned')
INSERT INTO Shifts VALUES('2','2018-02-07','9','17','Manager','Supervise Employees in your department','Assigned')
INSERT INTO Shifts VALUES('2','2018-02-08','9','17','Manager','Supervise Employees in your department','Assigned')
INSERT INTO Shifts VALUES('2','2018-02-09','9','17','Manager','Supervise Employees in your department','Assigned')
INSERT INTO Shifts VALUES('2','2018-02-10','9','17','Manager','Supervise Employees in your department','Assigned')

INSERT INTO Shifts VALUES('3','2018-02-04','8','16','Sales Associate','Assist Customers in finding items','Assigned')
INSERT INTO Shifts VALUES('3','2018-02-05','8','16','Sales Associate','Assist Customers in finding items','Assigned')
INSERT INTO Shifts VALUES('3','2018-02-06','8','16','Sales Associate','Assist Customers in finding items','Assigned')
INSERT INTO Shifts VALUES('3','2018-02-07','8','16','Sales Associate','Assist Customers in finding items','Assigned')
INSERT INTO Shifts VALUES('3','2018-02-08','8','16','Sales Associate','Assist Customers in finding items','Assigned')
INSERT INTO Shifts VALUES('3','2018-02-09','8','16','Sales Associate','Assist Customers in finding items','Assigned')
INSERT INTO Shifts VALUES('3','2018-02-10','8','16','Sales Associate','Assist Customers in finding items','Assigned')

INSERT INTO Shifts VALUES('4','2018-02-04','9','17','Cashier','Cash Customers Out','Assigned')
INSERT INTO Shifts VALUES('4','2018-02-05','9','17','Cashier','Cash Customers Out','Assigned')
INSERT INTO Shifts VALUES('4','2018-02-06','9','17','Cashier','Cash Customers Out','Assigned')
INSERT INTO Shifts VALUES('4','2018-02-07','9','17','Cashier','Cash Customers Out','Assigned')
INSERT INTO Shifts VALUES('4','2018-02-08','9','17','Cashier','Cash Customers Out','Assigned')
INSERT INTO Shifts VALUES('4','2018-02-09','9','17','Cashier','Cash Customers Out','Assigned')
INSERT INTO Shifts VALUES('4','2018-02-10','9','17','Cashier','Cash Customers Out','Assigned')

INSERT INTO Shifts VALUES('5','2018-02-04','9','17','Music Instructor','Gives Music Lessons on a Desired Instrument','Assigned')
INSERT INTO Shifts VALUES('5','2018-02-05','9','17','Music Instructor','Gives Music Lessons on a Desired Instrument','Assigned')
INSERT INTO Shifts VALUES('5','2018-02-06','9','17','Music Instructor','Gives Music Lessons on a Desired Instrument','Assigned')
INSERT INTO Shifts VALUES('5','2018-02-07','9','17','Music Instructor','Gives Music Lessons on a Desired Instrument','Assigned')
INSERT INTO Shifts VALUES('5','2018-02-08','9','17','Music Instructor','Gives Music Lessons on a Desired Instrument','Assigned')
INSERT INTO Shifts VALUES('5','2018-02-09','9','17','Music Instructor','Gives Music Lessons on a Desired Instrument','Assigned')
INSERT INTO Shifts VALUES('5','2018-02-10','9','17','Music Instructor','Gives Music Lessons on a Desired Instrument','Assigned')

INSERT INTO Shifts VALUES('6','2018-02-04','17','23','Cashier','Cash Customers Out','Assigned')
INSERT INTO Shifts VALUES('6','2018-02-05','17','23','Cashier','Cash Customers Out','Assigned')
INSERT INTO Shifts VALUES('6','2018-02-06','17','23','Cashier','Cash Customers Out','Assigned')
INSERT INTO Shifts VALUES('6','2018-02-07','17','23','Cashier','Cash Customers Out','Assigned')
INSERT INTO Shifts VALUES('6','2018-02-08','17','23','Cashier','Cash Customers Out','Assigned')
INSERT INTO Shifts VALUES('6','2018-02-09','17','23','Cashier','Cash Customers Out','Assigned')
INSERT INTO Shifts VALUES('6','2018-02-10','17','23','Cashier','Cash Customers Out','Assigned')

INSERT INTO Shifts VALUES('7','2018-02-04','8','16','Music Instructor','Gives Music Lessons on a Desired Instrument','Assigned')
INSERT INTO Shifts VALUES('7','2018-02-05','8','16','Music Instructor','Gives Music Lessons on a Desired Instrument','Assigned')
INSERT INTO Shifts VALUES('7','2018-02-06','8','16','Music Instructor','Gives Music Lessons on a Desired Instrument','Assigned')
INSERT INTO Shifts VALUES('7','2018-02-07','8','16','Music Instructor','Gives Music Lessons on a Desired Instrument','Assigned')
INSERT INTO Shifts VALUES('7','2018-02-08','8','16','Music Instructor','Gives Music Lessons on a Desired Instrument','Assigned')
INSERT INTO Shifts VALUES('7','2018-02-09','8','16','Music Instructor','Gives Music Lessons on a Desired Instrument','Assigned')
INSERT INTO Shifts VALUES('7','2018-02-10','8','16','Music Instructor','Gives Music Lessons on a Desired Instrument','Assigned')

INSERT INTO Shifts VALUES('8','2018-02-04','9','17','Music Instructor','Gives Music Lessons on a Desired Instrument','Assigned')
INSERT INTO Shifts VALUES('8','2018-02-05','9','17','Music Instructor','Gives Music Lessons on a Desired Instrument','Assigned')
INSERT INTO Shifts VALUES('8','2018-02-06','9','17','Music Instructor','Gives Music Lessons on a Desired Instrument','Assigned')
INSERT INTO Shifts VALUES('8','2018-02-07','9','17','Music Instructor','Gives Music Lessons on a Desired Instrument','Assigned')
INSERT INTO Shifts VALUES('8','2018-02-08','9','17','Music Instructor','Gives Music Lessons on a Desired Instrument','Assigned')
INSERT INTO Shifts VALUES('8','2018-02-09','9','17','Music Instructor','Gives Music Lessons on a Desired Instrument','Assigned')
INSERT INTO Shifts VALUES('8','2018-02-10','9','17','Music Instructor','Gives Music Lessons on a Desired Instrument','Assigned')

INSERT INTO Shifts VALUES('9','2018-02-04','9','17','Music Instructor','Gives Music Lessons on a Desired Instrument','Assigned')
INSERT INTO Shifts VALUES('9','2018-02-05','9','17','Music Instructor','Gives Music Lessons on a Desired Instrument','Assigned')
INSERT INTO Shifts VALUES('9','2018-02-06','9','17','Music Instructor','Gives Music Lessons on a Desired Instrument','Assigned')
INSERT INTO Shifts VALUES('9','2018-02-07','9','17','Music Instructor','Gives Music Lessons on a Desired Instrument','Assigned')
INSERT INTO Shifts VALUES('9','2018-02-08','9','17','Music Instructor','Gives Music Lessons on a Desired Instrument','Assigned')
INSERT INTO Shifts VALUES('9','2018-02-09','9','17','Music Instructor','Gives Music Lessons on a Desired Instrument','Assigned')
INSERT INTO Shifts VALUES('9','2018-02-10','9','17','Music Instructor','Gives Music Lessons on a Desired Instrument','Assigned')

INSERT INTO Shifts VALUES('10','2018-02-04','16','23','Manager','Supervise Employees in your department','Assigned')
INSERT INTO Shifts VALUES('10','2018-02-05','16','23','Manager','Supervise Employees in your department','Assigned')
INSERT INTO Shifts VALUES('10','2018-02-06','16','23','Manager','Supervise Employees in your department','Assigned')
INSERT INTO Shifts VALUES('10','2018-02-07','16','23','Manager','Supervise Employees in your department','Assigned')
INSERT INTO Shifts VALUES('10','2018-02-08','16','23','Manager','Supervise Employees in your department','Assigned')
INSERT INTO Shifts VALUES('10','2018-02-09','16','23','Manager','Supervise Employees in your department','Assigned')
INSERT INTO Shifts VALUES('10','2018-02-10','16','23','Manager','Supervise Employees in your department','Assigned')

INSERT INTO Shifts VALUES('11','2018-02-04','16','23','Sales Associate','Assist Customers in finding items','Assigned')
INSERT INTO Shifts VALUES('11','2018-02-05','16','23','Sales Associate','Assist Customers in finding items','Assigned')
INSERT INTO Shifts VALUES('11','2018-02-06','16','23','Sales Associate','Assist Customers in finding items','Assigned')
INSERT INTO Shifts VALUES('11','2018-02-07','16','23','Sales Associate','Assist Customers in finding items','Assigned')
INSERT INTO Shifts VALUES('11','2018-02-08','16','23','Sales Associate','Assist Customers in finding items','Assigned')
INSERT INTO Shifts VALUES('11','2018-02-09','16','23','Sales Associate','Assist Customers in finding items','Assigned')
INSERT INTO Shifts VALUES('11','2018-02-10','16','23','Sales Associate','Assist Customers in finding items','Assigned')

INSERT INTO Shifts VALUES('12','2018-02-04','15','22','Cashier','Cash Customers Out','Assigned')
INSERT INTO Shifts VALUES('12','2018-02-05','15','22','Cashier','Cash Customers Out','Assigned')
INSERT INTO Shifts VALUES('12','2018-02-06','15','22','Cashier','Cash Customers Out','Assigned')
INSERT INTO Shifts VALUES('12','2018-02-07','15','22','Cashier','Cash Customers Out','Assigned')
INSERT INTO Shifts VALUES('12','2018-02-08','15','22','Cashier','Cash Customers Out','Assigned')
INSERT INTO Shifts VALUES('12','2018-02-09','15','22','Cashier','Cash Customers Out','Assigned')
INSERT INTO Shifts VALUES('12','2018-02-10','15','22','Cashier','Cash Customers Out','Assigned')

INSERT INTO Shifts VALUES('13','2018-02-04','10','18','Cashier','Cash Customers Out','Assigned')
INSERT INTO Shifts VALUES('13','2018-02-05','10','18','Cashier','Cash Customers Out','Assigned')
INSERT INTO Shifts VALUES('13','2018-02-06','10','18','Cashier','Cash Customers Out','Assigned')
INSERT INTO Shifts VALUES('13','2018-02-07','10','18','Cashier','Cash Customers Out','Assigned')
INSERT INTO Shifts VALUES('13','2018-02-08','10','18','Cashier','Cash Customers Out','Assigned')
INSERT INTO Shifts VALUES('13','2018-02-09','10','18','Cashier','Cash Customers Out','Assigned')
INSERT INTO Shifts VALUES('13','2018-02-10','10','18','Cashier','Cash Customers Out','Assigned')

INSERT INTO Shifts VALUES('14','2018-02-04','8','16','Music Lessons Secretary','Sign Customers Up to Music Lessons','Assigned')
INSERT INTO Shifts VALUES('14','2018-02-05','8','16','Music Lessons Secretary','Sign Customers Up to Music Lessons','Assigned')
INSERT INTO Shifts VALUES('14','2018-02-06','8','16','Music Lessons Secretary','Sign Customers Up to Music Lessons','Assigned')
INSERT INTO Shifts VALUES('14','2018-02-07','8','16','Music Lessons Secretary','Sign Customers Up to Music Lessons','Assigned')
INSERT INTO Shifts VALUES('14','2018-02-08','8','16','Music Lessons Secretary','Sign Customers Up to Music Lessons','Assigned')
INSERT INTO Shifts VALUES('14','2018-02-09','8','16','Music Lessons Secretary','Sign Customers Up to Music Lessons','Assigned')
INSERT INTO Shifts VALUES('14','2018-02-10','8','16','Music Lessons Secretary','Sign Customers Up to Music Lessons','Assigned')


INSERT INTO AbsenceClaims VALUES('3','2018-02-04','2018-03-06','Wedding','Book Off',NULL)
INSERT INTO AbsenceClaims VALUES('6','2018-02-07','2018-03-08','Not Feeling Well','Illness',NULL)
INSERT INTO AbsenceClaims VALUES('13','2018-02-08','2018-03-10','Death Of Family Member','Bereavement',NULL)
INSERT INTO AbsenceClaims VALUES('4','2018-02-04','2018-03-05','Snowed In','Natural Disaster',NULL)
INSERT INTO AbsenceClaims VALUES('12','2018-02-10','2019-02-10','Maternity Leave','Leave Of Absence',NULL)


-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------


