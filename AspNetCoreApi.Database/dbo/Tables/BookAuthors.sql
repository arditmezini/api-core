CREATE TABLE [dbo].[BookAuthors]
(
	BookId INT IDENTITY(1,1) NOT NULL,
	AuthorId INT NOT NULL,
)
GO

ALTER TABLE [dbo].[BookAuthors]
	ADD CONSTRAINT [PK_BookAuthors] PRIMARY KEY ([BookId] DESC)
GO

ALTER TABLE [dbo].[BookAuthors]
	ADD CONSTRAINT [FK_BookAuthors_Book] FOREIGN KEY ([BookId]) REFERENCES [dbo].[Book] ([Id])
GO

ALTER TABLE [dbo].[BookAuthors]
	ADD CONSTRAINT [FK_BookAuthors_Author] FOREIGN KEY ([AuthorId]) REFERENCES [dbo].[Author] ([Id])
GO