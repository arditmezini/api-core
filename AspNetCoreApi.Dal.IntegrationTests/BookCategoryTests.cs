using AspNetCoreApi.Dal.Core.Contracts;
using AspNetCoreApi.Dal.IntegrationTests.Mocks;
using AspNetCoreApi.Models.Entity;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace AspNetCoreApi.Dal.IntegrationTests
{
    public class BookCategoryTests
    {
        private readonly Mock<IBookCategoryRepository> _mockBookCategoryRepository;

        public BookCategoryTests()
        {
            _mockBookCategoryRepository = BookCategoryRepositoryMock.GetBookCategoryRepository();
        }

        [Fact]
        public async Task RetriveAllBookCategories_Successfully()
        {
            var entity = new BookCategory { Name = "Category 4" };
            await _mockBookCategoryRepository.Object.Add(entity);

            var allBookCategories = await _mockBookCategoryRepository.Object.GetAll();

            Assert.IsType<List<BookCategory>>(allBookCategories);
            Assert.Equal(4, allBookCategories.Count());
        }
    }
}