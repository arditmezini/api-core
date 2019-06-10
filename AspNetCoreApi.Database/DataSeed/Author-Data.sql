SET IDENTITY_INSERT [dbo].[Author] ON;

INSERT INTO [dbo].[Author] 
	([Id],[FirstName],[LastName],[DateCreated],[UserCreated],[DateModified],[UserModified],[IsDeleted])
VALUES 
	(1,'Marcel','Proust',GETDATE(),'admin',NULL,NULL,0),
	(2,'Miguel','de Cervantes',GETDATE(),'admin',NULL,NULL,0), 
	(3,'James','Joyce',GETDATE(),'admin',NULL,NULL,0),
	(4,'F. Scott','Fitzgerald',GETDATE(),'admin',NULL,NULL,0),
	(5,'Herman ','Melville' ,GETDATE(),'admin',NULL,NULL,0);

SET IDENTITY_INSERT [dbo].[Author] OFF;
GO