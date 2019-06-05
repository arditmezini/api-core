GO

INSERT INTO [dbo].[AuthorContact]
           ([ContactNumber],[Address],[DateCreated],[UserCreated],[DateModified],[UserModified],[IsDeleted])
     VALUES
           ('154864824','Devoll',     GETDATE(),'admin',NULL,NULL,0),
		   ('265482156','Gjirokastër',GETDATE(),'admin',NULL,NULL,0),
		   ('361856548','Konicë',     GETDATE(),'admin',NULL,NULL,0),
		   ('412667953','Pozharan',   GETDATE(),'admin',NULL,NULL,0),
		   ('536598564','Tërstenik',  GETDATE(),'admin',NULL,NULL,0)

GO