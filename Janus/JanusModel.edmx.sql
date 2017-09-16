
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 09/16/2017 18:37:49
-- Generated from EDMX file: C:\Users\Matt Martin\Desktop\Janus\Janus\JanusModel.edmx
-- --------------------------------------------------

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

-- Creating table 'Employees'
CREATE TABLE [dbo].[Employees] (
    [employeeID] int IDENTITY(1,1) NOT NULL,
    [userID] int  NOT NULL,
    [availibilityID] int  NOT NULL,
    [hireDate] datetime  NOT NULL,
    [fireDate] datetime  NOT NULL,
    [position] nvarchar(max)  NOT NULL,
    [section] nvarchar(max)  NOT NULL,
    [manager] nvarchar(max)  NOT NULL,
    [employmentStatus] nvarchar(max)  NOT NULL,
    [Availibility_availibilityID] int  NOT NULL,
    [Company_companyID] int  NOT NULL,
    [Address_addressID] int  NOT NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [userID] int IDENTITY(1,1) NOT NULL,
    [addressID] int  NOT NULL,
    [contactID] int  NOT NULL,
    [companyID] int  NOT NULL,
    [roleID] int  NOT NULL,
    [firstName] nvarchar(max)  NOT NULL,
    [lastName] nvarchar(max)  NOT NULL,
    [birthDate] datetime  NOT NULL,
    [password] nvarchar(max)  NOT NULL,
    [Role_roleID] int  NOT NULL,
    [Recovery_recoveryID] int  NOT NULL,
    [Employee_employeeID] int  NOT NULL
);
GO

-- Creating table 'Availibilities'
CREATE TABLE [dbo].[Availibility] (
    [availibilityID] int IDENTITY(1,1) NOT NULL,
    [employeeID] int  NOT NULL,
    [startTime] datetime  NOT NULL,
    [endTime] datetime  NOT NULL,
    [day] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Shifts'
CREATE TABLE [dbo].[Shifts] (
    [shiftID] int IDENTITY(1,1) NOT NULL,
    [employeeID] int  NOT NULL,
    [shiftStart] datetime  NOT NULL,
    [shiftEnd] datetime  NOT NULL,
    [position] nvarchar(max)  NOT NULL,
    [description] nvarchar(max)  NOT NULL,
    [status] nvarchar(max)  NOT NULL,
    [Employee_employeeID] int  NOT NULL
);
GO

-- Creating table 'Companies'
CREATE TABLE [dbo].[Company] (
    [companyID] int IDENTITY(1,1) NOT NULL,
    [contactID] int  NOT NULL,
    [managerID] bigint  NOT NULL,
    [companyName] nvarchar(max)  NOT NULL,
    [hours] nvarchar(max)  NOT NULL,
    [day] nvarchar(max)  NOT NULL,
    [companyOwner] int  NOT NULL,
    [ContactInfo_contactID] int  NOT NULL
);
GO

-- Creating table 'AbsenceClaims'
CREATE TABLE [dbo].[AbsenceClaims] (
    [claimID] int IDENTITY(1,1) NOT NULL,
    [employeeID] int  NOT NULL,
    [managerID] int  NOT NULL,
    [startTime] datetime  NOT NULL,
    [endTime] datetime  NOT NULL,
    [description] nvarchar(max)  NOT NULL,
    [claimType] nvarchar(max)  NOT NULL,
    [isApproved] bit  NOT NULL,
    [Employee_employeeID] int  NOT NULL
);
GO

-- Creating table 'shiftRequests'
CREATE TABLE [dbo].[shiftRequests] (
    [shiftRequestID] int IDENTITY(1,1) NOT NULL,
    [managerID] int  NOT NULL,
    [requestor] nvarchar(max)  NOT NULL,
    [requestWith] nvarchar(max)  NOT NULL,
    [requestConfirmed] bit  NOT NULL,
    [requestStatus] nvarchar(max)  NOT NULL,
    [Employee_employeeID] int  NOT NULL
);
GO

-- Creating table 'Messages'
CREATE TABLE [dbo].[Messages] (
    [messageID] int IDENTITY(1,1) NOT NULL,
    [subject] nvarchar(max)  NOT NULL,
    [body] nvarchar(max)  NOT NULL,
    [mailFromUserID] int  NOT NULL,
    [mailToUserID] int  NOT NULL,
    [User_userID] int  NOT NULL
);
GO

-- Creating table 'Recoveries'
CREATE TABLE [dbo].[Recoveries] (
    [recoveryID] int IDENTITY(1,1) NOT NULL,
    [userID] int  NOT NULL,
    [questionID] int  NOT NULL,
    [userAnswer] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Roles'
CREATE TABLE [dbo].[Roles] (
    [roleID] int IDENTITY(1,1) NOT NULL,
    [userID] nvarchar(max)  NOT NULL,
    [role] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Questions'
CREATE TABLE [dbo].[Questions] (
    [questionID] int IDENTITY(1,1) NOT NULL,
    [question] nvarchar(max)  NOT NULL,
    [Recovery_recoveryID] int  NOT NULL
);
GO

-- Creating table 'Addresses'
CREATE TABLE [dbo].[Addresses] (
    [addressID] int IDENTITY(1,1) NOT NULL,
    [streetAddress] nvarchar(max)  NOT NULL,
    [postalCode] nvarchar(max)  NOT NULL,
    [Company_companyID] int  NOT NULL
);
GO

-- Creating table 'ContactInfoes'
CREATE TABLE [dbo].[ContactInfo] (
    [contactID] int IDENTITY(1,1) NOT NULL,
    [phone] nvarchar(max)  NOT NULL,
    [email] nvarchar(max)  NOT NULL,
    [User_userID] int  NOT NULL
);
GO

-- Creating table 'Managers'
CREATE TABLE [dbo].[Managers] (
    [managerID] int IDENTITY(1,1) NOT NULL,
    [employeeID] nvarchar(max)  NOT NULL,
    [isAdmin] nvarchar(max)  NOT NULL,
    [Company_companyID] int  NOT NULL,
    [Employee_employeeID] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [employeeID] in table 'Employees'
ALTER TABLE [dbo].[Employees]
ADD CONSTRAINT [PK_Employees]
    PRIMARY KEY CLUSTERED ([employeeID] ASC);
GO

-- Creating primary key on [userID] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([userID] ASC);
GO

-- Creating primary key on [availibilityID] in table 'Availibilities'
ALTER TABLE [dbo].[Availibility]
ADD CONSTRAINT [PK_Availibility]
    PRIMARY KEY CLUSTERED ([availibilityID] ASC);
GO

-- Creating primary key on [shiftID] in table 'Shifts'
ALTER TABLE [dbo].[Shifts]
ADD CONSTRAINT [PK_Shifts]
    PRIMARY KEY CLUSTERED ([shiftID] ASC);
GO

-- Creating primary key on [companyID] in table 'Companies'
ALTER TABLE [dbo].[Company]
ADD CONSTRAINT [PK_Company]
    PRIMARY KEY CLUSTERED ([companyID] ASC);
GO

-- Creating primary key on [claimID] in table 'AbsenceClaims'
ALTER TABLE [dbo].[AbsenceClaims]
ADD CONSTRAINT [PK_AbsenceClaims]
    PRIMARY KEY CLUSTERED ([claimID] ASC);
GO

-- Creating primary key on [shiftRequestID] in table 'shiftRequests'
ALTER TABLE [dbo].[shiftRequests]
ADD CONSTRAINT [PK_shiftRequests]
    PRIMARY KEY CLUSTERED ([shiftRequestID] ASC);
GO

-- Creating primary key on [messageID] in table 'Messages'
ALTER TABLE [dbo].[Messages]
ADD CONSTRAINT [PK_Messages]
    PRIMARY KEY CLUSTERED ([messageID] ASC);
GO

-- Creating primary key on [recoveryID] in table 'Recoveries'
ALTER TABLE [dbo].[Recoveries]
ADD CONSTRAINT [PK_Recoveries]
    PRIMARY KEY CLUSTERED ([recoveryID] ASC);
GO

-- Creating primary key on [roleID] in table 'Roles'
ALTER TABLE [dbo].[Roles]
ADD CONSTRAINT [PK_Roles]
    PRIMARY KEY CLUSTERED ([roleID] ASC);
GO

-- Creating primary key on [questionID] in table 'Questions'
ALTER TABLE [dbo].[Questions]
ADD CONSTRAINT [PK_Questions]
    PRIMARY KEY CLUSTERED ([questionID] ASC);
GO

-- Creating primary key on [addressID] in table 'Addresses'
ALTER TABLE [dbo].[Addresses]
ADD CONSTRAINT [PK_Addresses]
    PRIMARY KEY CLUSTERED ([addressID] ASC);
GO

-- Creating primary key on [contactID] in table 'ContactInfoes'
ALTER TABLE [dbo].[ContactInfo]
ADD CONSTRAINT [PK_ContactInfo]
    PRIMARY KEY CLUSTERED ([contactID] ASC);
GO

-- Creating primary key on [managerID] in table 'Managers'
ALTER TABLE [dbo].[Managers]
ADD CONSTRAINT [PK_Managers]
    PRIMARY KEY CLUSTERED ([managerID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Role_roleID] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [FK_UsersRole]
    FOREIGN KEY ([Role_roleID])
    REFERENCES [dbo].[Roles]
        ([roleID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UsersRole'
CREATE INDEX [IX_FK_UsersRole]
ON [dbo].[Users]
    ([Role_roleID]);
GO

-- Creating foreign key on [Availibility_availibilityID] in table 'Employees'
ALTER TABLE [dbo].[Employees]
ADD CONSTRAINT [FK_EmployeesAvailibility]
    FOREIGN KEY ([Availibility_availibilityID])
    REFERENCES [dbo].[Availibility]
        ([availibilityID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeesAvailibility'
CREATE INDEX [IX_FK_EmployeesAvailibility]
ON [dbo].[Employees]
    ([Availibility_availibilityID]);
GO

-- Creating foreign key on [Recovery_recoveryID] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [FK_UsersRecovery]
    FOREIGN KEY ([Recovery_recoveryID])
    REFERENCES [dbo].[Recoveries]
        ([recoveryID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UsersRecovery'
CREATE INDEX [IX_FK_UsersRecovery]
ON [dbo].[Users]
    ([Recovery_recoveryID]);
GO

-- Creating foreign key on [Recovery_recoveryID] in table 'Questions'
ALTER TABLE [dbo].[Questions]
ADD CONSTRAINT [FK_QuestionsRecovery]
    FOREIGN KEY ([Recovery_recoveryID])
    REFERENCES [dbo].[Recoveries]
        ([recoveryID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_QuestionsRecovery'
CREATE INDEX [IX_FK_QuestionsRecovery]
ON [dbo].[Questions]
    ([Recovery_recoveryID]);
GO

-- Creating foreign key on [Employee_employeeID] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [FK_UsersEmployees]
    FOREIGN KEY ([Employee_employeeID])
    REFERENCES [dbo].[Employees]
        ([employeeID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UsersEmployees'
CREATE INDEX [IX_FK_UsersEmployees]
ON [dbo].[Users]
    ([Employee_employeeID]);
GO

-- Creating foreign key on [User_userID] in table 'Messages'
ALTER TABLE [dbo].[Messages]
ADD CONSTRAINT [FK_MessagesUsers]
    FOREIGN KEY ([User_userID])
    REFERENCES [dbo].[Users]
        ([userID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MessagesUsers'
CREATE INDEX [IX_FK_MessagesUsers]
ON [dbo].[Messages]
    ([User_userID]);
GO

-- Creating foreign key on [Employee_employeeID] in table 'shiftRequests'
ALTER TABLE [dbo].[shiftRequests]
ADD CONSTRAINT [FK_shiftRequestsEmployees]
    FOREIGN KEY ([Employee_employeeID])
    REFERENCES [dbo].[Employees]
        ([employeeID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_shiftRequestsEmployees'
CREATE INDEX [IX_FK_shiftRequestsEmployees]
ON [dbo].[shiftRequests]
    ([Employee_employeeID]);
GO

-- Creating foreign key on [Company_companyID] in table 'Employees'
ALTER TABLE [dbo].[Employees]
ADD CONSTRAINT [FK_CompanyEmployees]
    FOREIGN KEY ([Company_companyID])
    REFERENCES [dbo].[Company]
        ([companyID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CompanyEmployees'
CREATE INDEX [IX_FK_CompanyEmployees]
ON [dbo].[Employees]
    ([Company_companyID]);
GO

-- Creating foreign key on [Company_companyID] in table 'Managers'
ALTER TABLE [dbo].[Managers]
ADD CONSTRAINT [FK_CompanyManager]
    FOREIGN KEY ([Company_companyID])
    REFERENCES [dbo].[Company]
        ([companyID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CompanyManager'
CREATE INDEX [IX_FK_CompanyManager]
ON [dbo].[Managers]
    ([Company_companyID]);
GO

-- Creating foreign key on [Employee_employeeID] in table 'Shifts'
ALTER TABLE [dbo].[Shifts]
ADD CONSTRAINT [FK_EmployeesShifts]
    FOREIGN KEY ([Employee_employeeID])
    REFERENCES [dbo].[Employees]
        ([employeeID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeesShifts'
CREATE INDEX [IX_FK_EmployeesShifts]
ON [dbo].[Shifts]
    ([Employee_employeeID]);
GO

-- Creating foreign key on [Employee_employeeID] in table 'Managers'
ALTER TABLE [dbo].[Managers]
ADD CONSTRAINT [FK_ManagerEmployees]
    FOREIGN KEY ([Employee_employeeID])
    REFERENCES [dbo].[Employees]
        ([employeeID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ManagerEmployees'
CREATE INDEX [IX_FK_ManagerEmployees]
ON [dbo].[Managers]
    ([Employee_employeeID]);
GO

-- Creating foreign key on [Employee_employeeID] in table 'AbsenceClaims'
ALTER TABLE [dbo].[AbsenceClaims]
ADD CONSTRAINT [FK_AbsenceClaimsEmployees]
    FOREIGN KEY ([Employee_employeeID])
    REFERENCES [dbo].[Employees]
        ([employeeID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AbsenceClaimsEmployees'
CREATE INDEX [IX_FK_AbsenceClaimsEmployees]
ON [dbo].[AbsenceClaims]
    ([Employee_employeeID]);
GO

-- Creating foreign key on [Address_addressID] in table 'Employees'
ALTER TABLE [dbo].[Employees]
ADD CONSTRAINT [FK_AddressesEmployees]
    FOREIGN KEY ([Address_addressID])
    REFERENCES [dbo].[Addresses]
        ([addressID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AddressesEmployees'
CREATE INDEX [IX_FK_AddressesEmployees]
ON [dbo].[Employees]
    ([Address_addressID]);
GO

-- Creating foreign key on [Company_companyID] in table 'Addresses'
ALTER TABLE [dbo].[Addresses]
ADD CONSTRAINT [FK_AddressesCompany]
    FOREIGN KEY ([Company_companyID])
    REFERENCES [dbo].[Company]
        ([companyID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AddressesCompany'
CREATE INDEX [IX_FK_AddressesCompany]
ON [dbo].[Addresses]
    ([Company_companyID]);
GO

-- Creating foreign key on [ContactInfo_contactID] in table 'Companies'
ALTER TABLE [dbo].[Company]
ADD CONSTRAINT [FK_CompanyContactInfo]
    FOREIGN KEY ([ContactInfo_contactID])
    REFERENCES [dbo].[ContactInfo]
        ([contactID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CompanyContactInfo'
CREATE INDEX [IX_FK_CompanyContactInfo]
ON [dbo].[Company]
    ([ContactInfo_contactID]);
GO

-- Creating foreign key on [User_userID] in table 'ContactInfoes'
ALTER TABLE [dbo].[ContactInfo]
ADD CONSTRAINT [FK_ContactInfoUsers]
    FOREIGN KEY ([User_userID])
    REFERENCES [dbo].[Users]
        ([userID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ContactInfoUsers'
CREATE INDEX [IX_FK_ContactInfoUsers]
ON [dbo].[ContactInfo]
    ([User_userID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------