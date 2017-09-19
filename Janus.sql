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


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------



-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Questions'
CREATE TABLE [dbo].[Questions] (
    [questionID] int IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [question] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Addresses'
CREATE TABLE [dbo].[Addresses] (
    [addressID] int IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [streetAddress] nvarchar(max)  NOT NULL,
    [postalCode] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'ContactInfoes'
CREATE TABLE [dbo].[ContactInfo] (
    [contactID] int IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [phone] nvarchar(max)  NOT NULL,
    [email] nvarchar(max)  NOT NULL
);
GO


-- Creating table 'Messages'
CREATE TABLE [dbo].[Messages] (
    [messageID] int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[mailFromUserID] int  NOT NULL,
    [mailToUserID] int  NOT NULL,
    [subject] nvarchar(max)  NOT NULL,
    [body] nvarchar(max)  NOT NULL

);
GO

-- Creating table 'Managers'
CREATE TABLE [dbo].[Managers] (
    [managerID] int IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [employeeID] int NOT NULL,
    [isAdmin] bit NOT NULL
);
GO

-- Creating table 'Availibilities'
CREATE TABLE [dbo].[Availibility] (
    [availibilityID] int IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [employeeID] int  NOT NULL,
    [startTime] datetime  NOT NULL,
    [endTime] datetime  NOT NULL,
    [day] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Employees'
CREATE TABLE [dbo].[Employees] (
	employeeID int IDENTITY(1,1) NOT NULL PRIMARY KEY,
    userID int  NOT NULL,
    availibilityID int  NOT NULL FOREIGN KEY REFERENCES Availibility(availibilityID),
	managerID int  NOT NULL FOREIGN KEY REFERENCES Managers(managerID),
    hireDate datetime  NOT NULL,
    fireDate datetime  NOT NULL,
    position nvarchar(max)  NOT NULL,
    section nvarchar(max)  NOT NULL,
    employmentStatus nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Companies'
CREATE TABLE [dbo].[Company] (
    [companyID] int IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [contactID] int  NOT NULL FOREIGN KEY REFERENCES ContactInfo(contactID),
    [managerID] int  NOT NULL FOREIGN KEY REFERENCES Managers(managerID),
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

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    userID int IDENTITY(1,1) NOT NULL PRIMARY KEY,
    addressID int  NOT NULL FOREIGN KEY REFERENCES Addresses(addressID),
    contactID int  NOT NULL FOREIGN KEY REFERENCES ContactInfo(contactID),
    companyID int  NOT NULL FOREIGN KEY REFERENCES Company(companyID),
    roleID int  NOT NULL FOREIGN KEY REFERENCES Roles(roleID),
    firstName nvarchar(max)  NOT NULL,
    lastName nvarchar(max)  NOT NULL,
    birthDate datetime  NOT NULL,
    [password] nvarchar(max)  NOT NULL
);
GO



-- Creating table 'Shifts'
CREATE TABLE [dbo].[Shifts] (
    [shiftID] int IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [employeeID] int  NOT NULL FOREIGN KEY REFERENCES Employees(employeeID),
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
    [employeeID] int  NOT NULL FOREIGN KEY REFERENCES Employees(employeeID),
    [managerID] int  NOT NULL FOREIGN KEY REFERENCES Managers(managerID),
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
    [managerID] int  NOT NULL FOREIGN KEY REFERENCES Managers(managerID),
    [requestor] int  NOT NULL FOREIGN KEY REFERENCES Users(UserID),
    [requestWith] int  NOT NULL FOREIGN KEY REFERENCES Users(UserID),
    [requestConfirmed] bit  NOT NULL,
    [requestStatus] nvarchar(max)  NOT NULL
);
GO



-- Creating table 'Recoveries'
CREATE TABLE [dbo].[Recoveries] (
    [recoveryID] int IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [userID] int  NOT NULL FOREIGN KEY REFERENCES Users(userID),
    [questionID] int  NOT NULL FOREIGN KEY REFERENCES Questions(questionID),
    [userAnswer] nvarchar(max)  NOT NULL
);
GO


-----------------------------------------------------
-- ADD FOREIGN KEY CONSTRAINTS
-----------------------------------------------------
ALTER TABLE [dbo].[Managers]
ADD CONSTRAINT [FK_Managers_employeeID] FOREIGN KEY (employeeID)
REFERENCES Employees(employeeID);

ALTER TABLE [dbo].[Availibility]
ADD CONSTRAINT [FK_Availibility_employeeID] FOREIGN KEY (employeeID)
REFERENCES Employees(employeeID);

ALTER TABLE [dbo].[Employees]
ADD CONSTRAINT [FK_Employees_userID] FOREIGN KEY (userID)
REFERENCES Users(userID);


ALTER TABLE [dbo].[Roles]
ADD CONSTRAINT [FK_Roles_userID] FOREIGN KEY (userID)
REFERENCES Users(userID);


ALTER TABLE [dbo].[Messages]
ADD CONSTRAINT [FK_MessagesFrom_userID] FOREIGN KEY (mailFromUserID)
REFERENCES Users(userID);


ALTER TABLE [dbo].[Messages]
ADD CONSTRAINT [FK_MessagesTo_userID] FOREIGN KEY (mailToUserID)
REFERENCES Users(userID);


-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------