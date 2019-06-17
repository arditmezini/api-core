CREATE TABLE [dbo].[AspNetUserRoles]
(
	[UserId] NVARCHAR(450) NOT NULL,
	[RoleId] NVARCHAR(450) NOT NULL
)
GO

ALTER TABLE [dbo].[AspNetUserRoles]
	ADD CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId] DESC, [RoleId] DESC)
GO

ALTER TABLE [dbo].[AspNetUserRoles]
	ADD CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId])
	REFERENCES [dbo].[AspNetRoles] ([Id]) ON DELETE CASCADE
GO

ALTER TABLE [dbo].[AspNetUserRoles]
	ADD CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId])
	REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
GO