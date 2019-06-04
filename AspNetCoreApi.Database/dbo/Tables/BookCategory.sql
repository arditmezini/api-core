CREATE TABLE [dbo].[BookCategory]
(
	[Id] INT IDENTITY(1,1) NOT NULL,
	[Name] NVARCHAR(50) NOT NULL,
	[DateCreated] DATETIME NOT NULL,
	[UserCreated] NVARCHAR(50) NOT NULL,
	[DateModified] DATETIME NOT NULL,
	[UserModified] NVARCHAR(50) NOT NULL,
	[IsDeleted] BIT NOT NULL
)
GO

ALTER TABLE [dbo].[BookCategory] ADD
	CONSTRAINT [PK_BookCategory] PRIMARY KEY ([Id] DESC)
GO