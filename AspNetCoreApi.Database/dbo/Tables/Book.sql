CREATE TABLE [dbo].[Book]
(
	[Id] INT IDENTITY(1,1) NOT NULL,
	[Title] NVARCHAR(100) NOT NULL,
	[CategoryId] INT NOT NULL,
	[PublisherId] INT NOT NULL,
	[DateCreaated] DATETIME NOT NULL,
	[UserCreated] NVARCHAR(50) NOT NULL,
	[DateModified] DATETIME NOT NULL,
	[UserModified] NVARCHAR(50) NOT NULL,
	[IsDeleted] BIT NOT NULL
)
GO

ALTER TABLE [dbo].[Book] 
	ADD	CONSTRAINT [PK_Book] PRIMARY KEY ([Id] DESC)
GO

ALTER TABLE [dbo].[Book] 
	ADD	CONSTRAINT [FK_Book_Category] FOREIGN KEY ([CategoryId]) REFERENCES [dbo].[BookCategory] ([Id])
GO

ALTER TABLE [dbo].[Book]
	ADD CONSTRAINT [FK_Book_Publisher] FOREIGN KEY ([PublisherId]) REFERENCES [dbo].[Publisher] ([Id])
GO