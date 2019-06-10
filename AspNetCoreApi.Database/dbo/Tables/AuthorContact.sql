CREATE TABLE [dbo].[AuthorContact]
(
	[AuthorId] INT IDENTITY(1,1) NOT NULL,
	[CountryId] INT NOT NULL,
	[ContactNumber] NVARCHAR(15) NOT NULL,
	[Address] NVARCHAR(100) NOT NULL,
	[DateCreated] DATETIME NOT NULL,
	[UserCreated] NVARCHAR(50) NOT NULL,
	[DateModified] DATETIME NULL,
	[UserModified] NVARCHAR(50) NULL,
	[IsDeleted] BIT NOT NULL
)
GO

ALTER TABLE [dbo].[AuthorContact] 
	ADD CONSTRAINT [PK_AuthorContact] PRIMARY KEY ([AuthorId] DESC)
GO

ALTER TABLE [dbo].[AuthorContact] 
	ADD	CONSTRAINT [FK_AuthorContact_Author] FOREIGN KEY ([AuthorId]) REFERENCES [dbo].[Author] ([Id])
GO

ALTER TABLE [dbo].[AuthorContact]
	ADD CONSTRAINT [FK_AuthorContract_Countries] FOREIGN KEY ([CountryId]) REFERENCES [dbo].[Countries] ([Id])
GO