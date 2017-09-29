/****************************************************************/
-- Script: janus.sql
-- Author: Matthew Martin 000338807
-- Date: September 18th, 2017
-- Description: Databse for Janus Capstone Project
/****************************************************************/

SET QUOTED_IDENTIFIER OFF;
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

IF (OBJECT_ID('FK_AbsenceClaims_managerID', 'F') IS NOT NULL)
BEGIN
    ALTER TABLE [dbo].[AbsenceClaims] DROP CONSTRAINT FK_AbsenceClaims_managerID
END

--- Availibility

IF (OBJECT_ID('FK_Availibility_userID', 'F') IS NOT NULL)
BEGIN
    ALTER TABLE [dbo].[Availibility] DROP CONSTRAINT FK_Availibility_userID
END

--- Company

IF (OBJECT_ID('FK_Company_contactID', 'F') IS NOT NULL)
BEGIN
    ALTER TABLE [dbo].[Company] DROP CONSTRAINT FK_Company_contactID
END
IF (OBJECT_ID('FK_Company_managerID', 'F') IS NOT NULL)
BEGIN
    ALTER TABLE [dbo].[Company] DROP CONSTRAINT FK_Company_managerID
END


--- Managers

IF (OBJECT_ID('FK_Managers_userID', 'F') IS NOT NULL)
BEGIN
    ALTER TABLE [dbo].[Managers] DROP CONSTRAINT FK_Managers_userID
END

--Messages
IF (OBJECT_ID('FK_MessagesFrom_userID', 'F') IS NOT NULL)
BEGIN
    ALTER TABLE [dbo].[Messages] DROP CONSTRAINT FK_MessagesFrom_userID
END

IF (OBJECT_ID('FK_MessagesTo_userID', 'F') IS NOT NULL)
BEGIN
    ALTER TABLE [dbo].[Messages] DROP CONSTRAINT FK_MessagesTo_userID
END

-- Recoveries

IF (OBJECT_ID('FK_Recoveries_userID ', 'F') IS NOT NULL)
BEGIN
    ALTER TABLE [dbo].[Recoveries] DROP CONSTRAINT FK_Recoveries_userID 
END

IF (OBJECT_ID('FK_Recoveries_questionID', 'F') IS NOT NULL)
BEGIN
    ALTER TABLE [dbo].[Recoveries] DROP CONSTRAINT FK_Recoveries_questionID
END

-- Roles

IF (OBJECT_ID('FK_Roles_userID', 'F') IS NOT NULL)
BEGIN
    ALTER TABLE [dbo].[Roles] DROP CONSTRAINT FK_Roles_userID
END

-- Shift Requests

IF (OBJECT_ID('FK_ShiftRequests_managerID', 'F') IS NOT NULL)
BEGIN
    ALTER TABLE [dbo].[shiftRequests] DROP CONSTRAINT FK_ShiftRequests_managerID
END

IF (OBJECT_ID('FK_ShiftRequestsRequestor_userID', 'F') IS NOT NULL)
BEGIN
    ALTER TABLE [dbo].[shiftRequests] DROP CONSTRAINT FK_ShiftRequestsRequestor_userID
END

IF (OBJECT_ID('FK_ShiftRequestsRequestwith_userID', 'F') IS NOT NULL)
BEGIN
    ALTER TABLE [dbo].[shiftRequests] DROP CONSTRAINT FK_ShiftRequestsRequestwith_userID
END

-- Shifts
IF (OBJECT_ID('FK_Shifts_userID', 'F') IS NOT NULL)
BEGIN
    ALTER TABLE [dbo].[Shifts] DROP CONSTRAINT FK_Shifts_userID
END

-- Users

IF (OBJECT_ID('FK_Users_companyID', 'F') IS NOT NULL)
BEGIN
    ALTER TABLE [dbo].[Users] DROP CONSTRAINT FK_Users_companyID
END

IF (OBJECT_ID('FK_Users_roleID', 'F') IS NOT NULL)
BEGIN
    ALTER TABLE [dbo].[Users] DROP CONSTRAINT FK_Users_roleID
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


-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------


-- Creating table 'Recoveries'
CREATE TABLE [dbo].[Recoveries] (
    [recoveryID] int IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [userID] int  NOT NULL,
    [question] nvarchar(max)  NOT NULL,
    [userAnswer] nvarchar(max)  NOT NULL
);
GO



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

-- Creating table 'Managers'
CREATE TABLE [dbo].[Managers] (
    [managerID] int IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [userID] int NOT NULL,
    [isAdmin] bit NOT NULL
);
GO

-- Creating table 'Availibilities'
CREATE TABLE [dbo].[Availibility] (
    [availibilityID] int IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [userID] int  NOT NULL,
    [startTime] datetime  NOT NULL,
    [endTime] datetime  NOT NULL,
    [day] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    userID int IDENTITY(1,1) NOT NULL PRIMARY KEY,
    companyID int  NOT NULL,
	availibilityID int  NOT NULL,
	managerID int  NOT NULL ,
    roleID int  NOT NULL,
    firstName nvarchar(max)  NOT NULL,
    lastName nvarchar(max)  NOT NULL,
    birthDate datetime  NOT NULL,
    [password] nvarchar(max)  NOT NULL,
    [phone] nvarchar(max)  NOT NULL,
    [email] nvarchar(max)  NOT NULL,
	[streetAddress] nvarchar(max)  NOT NULL,
    [postalCode] nvarchar(max)  NOT NULL,
    hireDate datetime  NOT NULL,
    fireDate datetime  NOT NULL,
    employmentStatus nvarchar(max)  NOT NULL
);
GO


-- Creating table 'Companies'
CREATE TABLE [dbo].[Company] (
    [companyID] int IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [companyName] nvarchar(max)  NOT NULL,
    [hours] nvarchar(max)  NOT NULL,
    [day] nvarchar(max)  NOT NULL,
    [companyOwner] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Roles'
CREATE TABLE [dbo].[Roles] (
    [roleID] int IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [userID] int NOT NULL,
    [role] nvarchar(max)  NOT NULL
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
    [managerID] int  NOT NULL,
    [startTime] datetime  NOT NULL,
    [endTime] datetime  NOT NULL,
    [description] nvarchar(max)  NOT NULL,
    [claimType] nvarchar(max)  NOT NULL,
    [isApproved] bit  NOT NULL
);
GO

-- Creating table 'shiftRequests'
CREATE TABLE [dbo].[shiftRequests] (
    [shiftRequestID] int IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [managerID] int  NOT NULL,
    [requestor] int  NOT NULL,
    [requestWith] int  NOT NULL,
    [requestConfirmed] bit  NOT NULL,
    [requestStatus] nvarchar(max)  NOT NULL
);
GO






-----------------------------------------------------
-- ADD FOREIGN KEY CONSTRAINTS
-----------------------------------------------------


--Absence Claims Table

ALTER TABLE [dbo].[AbsenceClaims]
ADD CONSTRAINT [FK_AbsenceClaims_userID] FOREIGN KEY (userID)
REFERENCES Users(userID);

ALTER TABLE [dbo].[AbsenceClaims]
ADD CONSTRAINT [FK_AbsenceClaims_managerID] FOREIGN KEY (managerID)
REFERENCES Managers(managerID);


-- Addresses Table

-- Availibility Table
ALTER TABLE [dbo].[Availibility]
ADD CONSTRAINT [FK_Availibility_userID] FOREIGN KEY (userID)
REFERENCES Users(userID);


-- Managers Table 

ALTER TABLE [dbo].[Managers]
ADD CONSTRAINT [FK_Managers_userID] FOREIGN KEY (userID)
REFERENCES Users(userID);

-- Messages Table

ALTER TABLE [dbo].[Messages]
ADD CONSTRAINT [FK_MessagesFrom_userID] FOREIGN KEY (mailFromUserID)
REFERENCES Users(userID);


ALTER TABLE [dbo].[Messages]
ADD CONSTRAINT [FK_MessagesTo_userID] FOREIGN KEY (mailToUserID)
REFERENCES Users(userID);

-- Recoveries Table
ALTER TABLE [dbo].[Recoveries]
ADD CONSTRAINT [FK_Recoveries_userID] FOREIGN KEY (userID)
REFERENCES Users(userID);




-- Roles Table

ALTER TABLE [dbo].[Roles]
ADD CONSTRAINT [FK_Roles_userID] FOREIGN KEY (userID)
REFERENCES Users(userID);

-- Shift Requests Table
ALTER TABLE [dbo].[ShiftRequests]
ADD CONSTRAINT [FK_ShiftRequests_managerID] FOREIGN KEY (managerID)
REFERENCES Managers(managerID);

ALTER TABLE [dbo].[ShiftRequests]
ADD CONSTRAINT [FK_ShiftRequestsRequestor_userID] FOREIGN KEY (requestor)
REFERENCES Users(userID);

ALTER TABLE [dbo].[ShiftRequests]
ADD CONSTRAINT [FK_ShiftRequestsRequestwith_userID] FOREIGN KEY (requestWith)
REFERENCES Users(userID);


-- Shifts Table 
ALTER TABLE [dbo].[Shifts]
ADD CONSTRAINT [FK_Shifts_userID] FOREIGN KEY (userID)
REFERENCES Users(userID);

-- Users Table

ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [FK_Users_companyID] FOREIGN KEY (companyID)
REFERENCES Company(companyID);

ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [FK_Users_roleID] FOREIGN KEY (roleID)
REFERENCES Roles(roleID);












-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------