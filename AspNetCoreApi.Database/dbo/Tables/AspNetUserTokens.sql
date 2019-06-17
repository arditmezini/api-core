CREATE TABLE [dbo].[AspNetUserTokens]
(
	[UserId] [nvarchar](450) NOT NULL,
	[LoginProvider] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](450) NOT NULL,
	[Value] [nvarchar](max) NULL
)
GO

ALTER TABLE [dbo].[AspNetUserTokens]
	ADD CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId] DESC, [LoginProvider] DESC, [Name] DESC) 
GO

ALTER TABLE [dbo].[AspNetUserTokens]
	ADD CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId])
	REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
GO