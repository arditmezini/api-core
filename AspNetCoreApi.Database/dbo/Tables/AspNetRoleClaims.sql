CREATE TABLE [dbo].[AspNetRoleClaims]
(
	[Id] INT IDENTITY(1,1) NOT NULL,
	[ClaimType] NVARCHAR(MAX) NULL,
	[ClaimValue] NVARCHAR(MAX) NULL,
	[RoleId] NVARCHAR(450) NOT NULL
)
GO

ALTER TABLE [dbo].[AspNetRoleClaims] 
	ADD CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id] DESC)
GO

ALTER TABLE [dbo].[AspNetRoleClaims]
	ADD CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) 
	REFERENCES [dbo].[AspNetRoles] ([Id]) ON DELETE CASCADE
GO
