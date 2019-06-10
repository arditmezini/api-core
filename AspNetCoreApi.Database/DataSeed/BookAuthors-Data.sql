SET IDENTITY_INSERT [dbo].[BookAuthors] ON;

INSERT INTO [dbo].[BookAuthors]
	([BookId],[AuthorId])
VALUES
	(1,1),
	(2,2),
	(3,3),
	(4,4),
	(5,5);

SET IDENTITY_INSERT [dbo].[BookAuthors] OFF;
GO