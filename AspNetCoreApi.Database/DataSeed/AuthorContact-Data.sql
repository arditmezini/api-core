SET IDENTITY_INSERT [dbo].[AuthorContact] ON;

INSERT INTO [dbo].[AuthorContact]
	([AuthorId],[CountryId], [ContactNumber], [Address], [DateCreated], [UserCreated], [DateModified], [UserModified], [IsDeleted])
VALUES
	(1,73,'154864824','Paris',GETDATE(),'admin',NULL,NULL,0),
	(2,199,'265482156','Alcalá de Henares',GETDATE(),'admin',NULL,NULL,0),
	(3,103,'361856548','Rathgar',GETDATE(),'admin',NULL,NULL,0),
	(4,226,'412667953','Minnesota',GETDATE(),'admin',NULL,NULL,0),
	(5,226,'536598564','New York',GETDATE(),'admin',NULL,NULL,0);

SET IDENTITY_INSERT [dbo].[AuthorContact] OFF;
GO