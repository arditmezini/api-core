CREATE TABLE [dbo].[Countries] (
  [Id]  INT IDENTITY(1,1) NOT NULL,
  [Iso]  VARCHAR(2) NOT NULL,
  [Name]  VARCHAR(80) NOT NULL,
  [Iso3]  VARCHAR(3) NULL,
  [NumCode] INT  NULL,
  [PhoneCode] INT NOT NULL,
  [DateCreated] DATETIME NOT NULL,
  [UserCreated] NVARCHAR(50) NOT NULL,
  [DateModified] DATETIME NULL,
  [UserModified] NVARCHAR(50) NULL,
  [IsDeleted] BIT NOT NULL
)
GO

ALTER TABLE [dbo].[Countries] 
	ADD	CONSTRAINT [PK_Countries] PRIMARY KEY ([Id] ASC)
GO

ALTER TABLE [dbo].[Countries]
	ADD CONSTRAINT [UNC_Countries_Iso] UNIQUE NONCLUSTERED ([Iso] ASC)
GO