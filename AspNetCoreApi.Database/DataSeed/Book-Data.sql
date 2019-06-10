SET IDENTITY_INSERT [dbo].[Book] ON;

INSERT INTO [dbo].[Book]
	([Id],[Title],[Isbn],[PublishedYear],[CategoryId],[PublisherId],[DateCreated],[UserCreated],[DateModified],[UserModified],[IsDeleted])
VALUES
    (1,'In Search of Lost Time','9780300185430',1913,1,1,GETDATE(),'admin',NULL,NULL,0),
	(2,'Don Quixote','9780393090185',1605,2,2,GETDATE(),'admin',NULL,NULL,0),
	(3,'Ulysses','9781107423909',1922,1,3,GETDATE(),'admin',NULL,NULL,0),
	(4,'The Great Gatsby','9780816082322',1925,2,4,GETDATE(),'admin',NULL,NULL,0),
	(5,'Moby Dick','9780321228000',1851,2,5,GETDATE(),'admin',NULL,NULL,0);

SET IDENTITY_INSERT [dbo].[Book] OFF;
GO