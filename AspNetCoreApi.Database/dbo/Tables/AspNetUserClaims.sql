CREATE TABLE [dbo].[AspNetUserClaims]
(
	[Id] INT IDENTITY(1,1) NOT NULL,
	[ClaimType] NVARCHAR(MAX) NULL,
	[ClaimValue] NVARCHAR(MAX) NULL,
	[UserId] NVARCHAR(450) NOT NULL
)
GO

ALTER TABLE [dbo].[AspNetUserClaims]
	ADD CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id] DESC)
GO

ALTER TABLE [dbo].[AspNetUserClaims]
	ADD CONSTRAINT [FK_AspNetUserClaims_AspNetUser_UserId] FOREIGN KEY ([UserId])
	REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
GO