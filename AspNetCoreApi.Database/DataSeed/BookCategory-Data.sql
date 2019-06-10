SET IDENTITY_INSERT [dbo].[BookCategory] ON;

INSERT INTO [dbo].[BookCategory]	
	([Id],[Name],[DateCreated],[UserCreated],[DateModified],[UserModified],[IsDeleted])
VALUES
    (1,'Modernist',GETDATE(),'admin',NULL,NULL,0),
	(2,'Novel',GETDATE(),'admin',NULL,NULL,0);

SET IDENTITY_INSERT [dbo].[BookCategory] OFF;
GO