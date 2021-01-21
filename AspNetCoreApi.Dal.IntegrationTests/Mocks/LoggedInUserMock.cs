using AspNetCoreApi.Dal.Core.Contracts;
using Moq;

namespace AspNetCoreApi.Dal.IntegrationTests.Mocks
{
    public class LoggedInUserMock
    {
        public const string _loggedInUserName = "Admin";

        public static Mock<ILoggedInUser> GetLoggedInUser()
        {
            var loggedInUser = new Mock<ILoggedInUser>();

            loggedInUser
                .Setup(o => o.Username)
                .Returns(_loggedInUserName);

            return loggedInUser;
        }
    }
}