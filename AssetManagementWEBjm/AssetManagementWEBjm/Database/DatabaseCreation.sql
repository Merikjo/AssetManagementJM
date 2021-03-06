﻿CREATE TABLE [dbo].[AssetLocation]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[Code] NVARCHAR(100),
	[Name] NVARCHAR(200),
	[Adress] NVARCHAR(500)
	)
	CREATE TABLE [dbo].[Assets]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[Code] NVARCHAR(100),
	[Type] NVARCHAR(500),
	[Model] NVARCHAR(500)
	)
	CREATE TABLE [dbo].[AssetLocations]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[LocationId] INT, 
	[AssetId] INT,
	[LastSeen] DATETIME,
	PRIMARY KEY CLUSTERED ([Id] ASC), 
    CONSTRAINT [FK_AssetLocations_Locations] FOREIGN KEY ([LocationId]) REFERENCES [AssetLocation]([Id]),
    CONSTRAINT [FK_AssetLocations_Assets] FOREIGN KEY ([AssetId]) REFERENCES [Assets]([Id])
);
	