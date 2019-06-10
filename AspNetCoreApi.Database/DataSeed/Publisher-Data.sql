SET IDENTITY_INSERT [dbo].[Publisher] ON;

INSERT INTO [dbo].[Publisher]
	([Id],[Name],[DateCreated],[UserCreated],[DateModified],[UserModified],[IsDeleted])
VALUES
    (1,'Grasset and Gallimard',GETDATE(),'admin',NULL,NULL,0),
	(2,'Francisco de Robles', GETDATE(),'admin',NULL,NULL,0),
	(3,'Sylvia Beach',GETDATE(),'admin',NULL,NULL,0),
	(4,'Charles Scribner''s Sons',GETDATE(),'admin',NULL,NULL,0),
	(5,'Harper & Brothers',GETDATE(),'admin',NULL,NULL,0);

SET IDENTITY_INSERT [dbo].[Publisher] OFF;
GO