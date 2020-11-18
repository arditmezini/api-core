CREATE VIEW [dbo].[Statistics]
AS
	SELECT DISTINCT COUNT(*) AS Count, 'Book' As Description FROM Book 
	UNION ALL
	SELECT DISTINCT COUNT(*) AS Count, 'Authors' As Description FROM Author 
	UNION ALL
	SELECT DISTINCT COUNT(*) AS Count, 'Category' As Description FROM BookCategory 
	UNION ALL
	SELECT DISTINCT COUNT(*) AS Count, 'Publisher' As Description FROM Publisher 
