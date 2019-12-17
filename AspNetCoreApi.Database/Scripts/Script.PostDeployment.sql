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

	PRINT('Countires Start')
		:r ..\DataSeed\Countries-Data.sql
	PRINT('Countries End')

	PRINT('AuthorData Start')
		:r ..\DataSeed\Author-Data.sql
	PRINT('AuthorData End')

	PRINT('AuthorContactData Start')
		:r ..\DataSeed\AuthorContact-Data.sql
	PRINT('AuthorContactData End')

	PRINT('BookCategoryData Start')
		:r ..\DataSeed\BookCategory-Data.sql
	PRINT('BookCategoryData End')

	PRINT('PublisherData Start')
		:r ..\DataSeed\Publisher-Data.sql
	PRINT('PublisherData End')

	PRINT('BookData Start')
		:r ..\DataSeed\Book-Data.sql
	PRINT('BookData End')

	PRINT('BookAuthorsData Start')
		:r ..\DataSeed\BookAuthors-Data.sql
	PRINT('BookAuthorsData End')

	PRINT('AspNetRoles Start')
		:r ..\DataSeed\AspNetRoles-Data.sql
	PRINT('AspNetRoles End')

PRINT('Post-Deployment Script END')