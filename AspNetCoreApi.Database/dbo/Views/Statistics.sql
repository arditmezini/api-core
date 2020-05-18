CREATE VIEW [dbo].[Statistics]
AS
	SELECT
        (SELECT DISTINCT COUNT(*) FROM Book) AS BookCount,
        (SELECT DISTINCT COUNT(*) FROM Author) AS AuthorCount,
		(SELECT DISTINCT COUNT(*) FROM BookCategory) AS CategoryCount,
		(SELECT DISTINCT COUNT(*) FROM Publisher) AS PublisherCount
