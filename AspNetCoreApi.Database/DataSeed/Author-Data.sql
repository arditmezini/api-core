GO

INSERT INTO [dbo].[Author] 
	([FirstName],[LastName],[Country],[DateCreated],[UserCreated],[DateModified],[UserModified],[IsDeleted])
VALUES 
	('Dritero','Agolli','Albania',GETDATE(),'admin',NULL,NULL,0),
	('Ismail' ,'Kadare','Albania',GETDATE(),'admin',NULL,NULL,0),
	('Faik'   ,'Konica','Albania',GETDATE(),'admin',NULL,NULL,0),
	('Sinan'  ,'Hasani','Kosovo' ,GETDATE(),'admin',NULL,NULL,0),
	('Rifat'  ,'Kukaj' ,'Kosovo' ,GETDATE(),'admin',NULL,NULL,0)

GO