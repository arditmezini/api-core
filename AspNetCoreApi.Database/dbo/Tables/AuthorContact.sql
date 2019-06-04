CREATE TABLE [dbo].[AuthorContact]
(
	[AuthorId] INT IDENTITY(1,1) NOT NULL,
	[ContactNumber] NVARCHAR(15) NULL,
	[Address] NVARCHAR(100) NULL,
	[DateCreated] DATETIME NOT NULL,
	[UserCreated] NVARCHAR(50) NOT NULL,
	[DateModified] DATETIME NOT NULL,
	[UserModified] NVARCHAR(50) NOT NULL,
	[IsDeleted] BIT NOT NULL
)
GO

ALTER TABLE [dbo].[AuthorContact] 
	ADD CONSTRAINT [PK_AuthorContact] PRIMARY KEY ([AuthorId] DESC)
GO

ALTER TABLE [dbo].[AuthorContact] 
	ADD	CONSTRAINT [FK_AuthorContact_Author] FOREIGN KEY ([AuthorId]) REFERENCES [dbo].[Author] ([Id])
GO
