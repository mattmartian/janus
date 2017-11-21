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

IF OBJECT_ID('dbo.Addresses', 'U') IS NOT NULL 
  DROP TABLE dbo.Addresses; 

IF OBJECT_ID('dbo.Availibility', 'U') IS NOT NULL 
  DROP TABLE dbo.Availibility; 

IF OBJECT_ID('dbo.Company', 'U') IS NOT NULL 
  DROP TABLE dbo.Company; 

IF OBJECT_ID('dbo.ContactInfo', 'U') IS NOT NULL 
  DROP TABLE dbo.ContactInfo; 

IF OBJECT_ID('dbo.Employees', 'U') IS NOT NULL 
  DROP TABLE dbo.Employees;

IF OBJECT_ID('dbo.Managers', 'U') IS NOT NULL 
  DROP TABLE dbo.Managers; 

IF OBJECT_ID('dbo.[Messages]', 'U') IS NOT NULL 
  DROP TABLE dbo.[Messages]; 

IF OBJECT_ID('dbo.Questions', 'U') IS NOT NULL 
  DROP TABLE dbo.Questions;
  
IF OBJECT_ID('dbo.Recoveries', 'U') IS NOT NULL 
  DROP TABLE dbo.Recoveries;  

IF OBJECT_ID('dbo.Roles', 'U') IS NOT NULL 
  DROP TABLE dbo.Roles; 

IF OBJECT_ID('dbo.shiftRequests', 'U') IS NOT NULL 
  DROP TABLE dbo.shiftRequests; 

IF OBJECT_ID('dbo.Shifts', 'U') IS NOT NULL 
  DROP TABLE dbo.Shifts; 

IF OBJECT_ID('dbo.Users', 'U') IS NOT NULL 
  DROP TABLE dbo.Users; 

IF OBJECT_ID('dbo.Departments', 'U') IS NOT NULL 
  DROP TABLE dbo.Departments; 


-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Messages'
CREATE TABLE [dbo].[Messages] (
    [messageID] int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[mailFromUserID] int  NOT NULL,
    [mailToUserID] int  NOT NULL,
    [subject] nvarchar(max)  NOT NULL,
    [body] nvarchar(max)  NOT NULL,
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
    [shiftStart] datetime  NOT NULL,
    [shiftEnd] datetime  NOT NULL,
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
    [requestWith] nvarchar(max)  NOT NULL,
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









Use Janus
EXEC dbo.SelectEmployee;
GO





-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------