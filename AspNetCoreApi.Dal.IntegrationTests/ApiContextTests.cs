using AspNetCoreApi.Dal.Core.Contracts;
using AspNetCoreApi.Dal.Entities;
using AspNetCoreApi.Dal.IntegrationTests.Mocks;
using AspNetCoreApi.Data.DataContext;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using Xunit;

namespace AspNetCoreApi.Dal.IntegrationTests
{
    public class ApiContextTests
    {
        private readonly ApiContext _apiContext;
        private readonly Mock<ILoggedInUser> _loggedInUser;

        public ApiContextTests()
        {
            var dbContextOptions = new DbContextOptionsBuilder<ApiContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            _loggedInUser = LoggedInUserMock.GetLoggedInUser();

            _apiContext = new ApiContext(dbContextOptions, _loggedInUser.Object);
        }
        
        [Fact]
        public async void SaveBookCategory_SetCreatedByProperty()
        {
            var bookCategory = new BookCategory { Name = "Test Category" };
            _apiContext.BookCategories.Add(bookCategory);
            await _apiContext.SaveChangesAsync();

            Assert.NotEmpty(bookCategory.UserCreated);
            Assert.Equal(_loggedInUser.Object.Username, bookCategory.UserCreated);
        }
    }
}