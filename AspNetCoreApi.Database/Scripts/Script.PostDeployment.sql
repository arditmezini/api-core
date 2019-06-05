/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

PRINT('Post-Deployment Script START')

	PRINT('Author Data Start')
		:r ..\DataSeed\Author-Data.sql
	PRINT('Author Data End')

	PRINT('AuthorContact Start')
		:r ..\DataSeed\AuthorContact-Data.sql
	PRINT('AuthorContact End')

PRINT('Post-Deployment Script END')