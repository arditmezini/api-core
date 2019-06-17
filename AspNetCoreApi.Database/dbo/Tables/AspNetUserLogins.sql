CREATE TABLE [dbo].[AspNetUserLogins]
(
	[LoginProvider] NVARCHAR(450) NOT NULL,
	[ProviderKey] NVARCHAR(450) NOT NULL,
	[ProviderDisplayName] NVARCHAR(MAX) NULL,
	[UserId] NVARCHAR(450) NOT NULL
)
GO

ALTER TABLE [dbo].[AspNetUserLogins]
	ADD CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider] DESC, [ProviderKey] DESC)
GO

ALTER TABLE [dbo].[AspNetUserLogins]
	ADD CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId])
	REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
GO