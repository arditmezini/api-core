CREATE TABLE [dbo].[SqlCache](
	[Id] [nvarchar](449) NOT NULL,
	[Value] [varbinary](max) NOT NULL,
	[ExpiresAtTime] [datetimeoffset](7) NOT NULL,
	[SlidingExpirationInSeconds] [bigint] NULL,
	[AbsoluteExpiration] [datetimeoffset](7) NULL,
	PRIMARY KEY (Id))

GO

CREATE NONCLUSTERED INDEX Index_ExpiresAtTime ON [dbo].[SqlCache] (ExpiresAtTime)