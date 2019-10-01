
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 11/07/2018 19:49:22
-- Generated from EDMX file: E:\VisualStudio2017\3.1.T.AssetManagementJM\AssetManagementWEBjm\AssetManagementWEBjm\Database\AssetsModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [AssetManageData];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_AssetLocations_Assets]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AssetLocations] DROP CONSTRAINT [FK_AssetLocations_Assets];
GO
IF OBJECT_ID(N'[dbo].[FK_AssetLocations_Locations]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AssetLocations] DROP CONSTRAINT [FK_AssetLocations_Locations];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[AssetLocation]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AssetLocation];
GO
IF OBJECT_ID(N'[dbo].[AssetLocations]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AssetLocations];
GO
IF OBJECT_ID(N'[dbo].[Assets]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Assets];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'AssetLocation'
CREATE TABLE [dbo].[AssetLocation] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Code] nvarchar(100)  NULL,
    [Name] nvarchar(200)  NULL,
    [Adress] nvarchar(500)  NULL
);
GO

-- Creating table 'AssetLocations'
CREATE TABLE [dbo].[AssetLocations] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [LocationId] int  NULL,
    [AssetId] int  NULL,
    [LastSeen] datetime  NULL
);
GO

-- Creating table 'Assets'
CREATE TABLE [dbo].[Assets] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Code] nvarchar(100)  NULL,
    [Type] nvarchar(500)  NULL,
    [Model] nvarchar(500)  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'AssetLocation'
ALTER TABLE [dbo].[AssetLocation]
ADD CONSTRAINT [PK_AssetLocation]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'AssetLocations'
ALTER TABLE [dbo].[AssetLocations]
ADD CONSTRAINT [PK_AssetLocations]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Assets'
ALTER TABLE [dbo].[Assets]
ADD CONSTRAINT [PK_Assets]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [LocationId] in table 'AssetLocations'
ALTER TABLE [dbo].[AssetLocations]
ADD CONSTRAINT [FK_AssetLocations_Locations]
    FOREIGN KEY ([LocationId])
    REFERENCES [dbo].[AssetLocation]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AssetLocations_Locations'
CREATE INDEX [IX_FK_AssetLocations_Locations]
ON [dbo].[AssetLocations]
    ([LocationId]);
GO

-- Creating foreign key on [AssetId] in table 'AssetLocations'
ALTER TABLE [dbo].[AssetLocations]
ADD CONSTRAINT [FK_AssetLocations_Assets]
    FOREIGN KEY ([AssetId])
    REFERENCES [dbo].[Assets]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AssetLocations_Assets'
CREATE INDEX [IX_FK_AssetLocations_Assets]
ON [dbo].[AssetLocations]
    ([AssetId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------