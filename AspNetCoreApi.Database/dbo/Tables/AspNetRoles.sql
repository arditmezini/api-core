CREATE TABLE [dbo].[AspNetRoles]
(
	[Id] NVARCHAR(450) NOT NULL,
	[ConcurrencyStamp] NVARCHAR(MAX) NULL,
	[Name] NVARCHAR(256) NULL,
	[NormalizedName] NVARCHAR(256) NULL
)
GO

ALTER TABLE [dbo].[AspNetRoles]
	ADD CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id] DESC)
GO
