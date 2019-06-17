CREATE TABLE [dbo].[AspNetUsers]
(
	[Id] NVARCHAR(450) NOT NULL,
	[AccessFailedCount] INT NOT NULL,
	[ConcurrencyStamp] NVARCHAR(MAX) NULL,
	[FirstName] NVARCHAR(100) NOT NULL,
	[LastName] NVARCHAR(100) NOT NULL,
	[Email] NVARCHAR(256) NULL,
	[EmailConfirmed] BIT NOT NULL,
	[LockoutEnabled] BIT NOT NULL,
	[LockoutEnd] DATETIMEOFFSET(7) NULL,
	[NormalizedEmail] NVARCHAR(256) NULL,
	[NormalizedUserName] NVARCHAR(256) NULL,
	[PasswordHash] NVARCHAR(MAX) NULL,
	[PhoneNumber] NVARCHAR(MAX) NULL,
	[PhoneNumberConfirmed] BIT NOT NULL,
	[SecurityStamp] NVARCHAR(MAX) NULL,
	[TwoFactorEnabled] BIT NOT NULL,
	[UserName] NVARCHAR(256) NULL,
)
GO

ALTER TABLE [dbo].[AspNetUsers]
	ADD CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id] DESC)
GO