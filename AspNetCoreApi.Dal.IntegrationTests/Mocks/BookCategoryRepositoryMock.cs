using AspNetCoreApi.Dal.Core.Contracts;
using AspNetCoreApi.Models.Entity;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspNetCoreApi.Dal.IntegrationTests.Mocks
{
    public class BookCategoryRepositoryMock
    {
        public static Mock<IBookCategoryRepository> GetBookCategoryRepository()
        {
            var bookCategories = new List<BookCategory>
            {
                new BookCategory { Name = "Category 1"},
                new BookCategory { Name = "Category 2"},
                new BookCategory { Name = "Category 3"}
            };

            var mockBookCategoryRepository = new Mock<IBookCategoryRepository>();

            mockBookCategoryRepository
                .Setup(repo => repo.GetAll())
                .ReturnsAsync(bookCategories);

            mockBookCategoryRepository
                .Setup(repo => repo.Add(It.IsAny<BookCategory>()))
                .Returns((BookCategory entity) =>
                {
                    bookCategories.Add(entity);
                    return Task.CompletedTask;
                });

            return mockBookCategoryRepository;
        }
    }
}