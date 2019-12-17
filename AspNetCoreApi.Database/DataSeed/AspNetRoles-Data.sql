INSERT INTO [dbo].[AspNetRoles]
	([Id], [ConcurrencyStamp], [Name], [NormalizedName])
VALUES
	(1, NEWID(), N'Admin', N'Admin'),
	(2, NEWID(), N'User', N'User'),
	(3, NEWID(), N'Manager', N'Manager')
GO