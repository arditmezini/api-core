﻿CREATE TABLE [dbo].[Publisher]
(
	[Id] INT IDENTITY(1,1) NOT NULL,
	[Name] NVARCHAR(100) NOT NULL,
	[DateCreated] DATETIME NOT NULL,
	[UserCreated] NVARCHAR(50) NOT NULL,
	[DateModified] DATETIME NULL,
	[UserModified] NVARCHAR(50) NULL,
	[IsDeleted] BIT NOT NULL
)
GO

ALTER TABLE [dbo].[Publisher] ADD
	CONSTRAINT [PK_Publisher] PRIMARY KEY ([Id] DESC)
GO