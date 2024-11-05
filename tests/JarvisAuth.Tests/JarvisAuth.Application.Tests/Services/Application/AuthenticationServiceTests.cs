using JarvisAuth.Application.Services.Authentication;
using JarvisAuth.Core.Messages;
using JarvisAuth.Core.Requests.Authentication;
using JarvisAuth.Domain.Entities;
using JarvisAuth.Domain.Interfaces.Repositories.Application;
using JarvisAuth.Domain.Interfaces.Repositories.User;
using Microsoft.Extensions.Configuration;
using Moq;

namespace JarvisAuth.Tests.JarvisAuth.Application.Tests.Services.Application
{
    public class AuthenticationServiceTests
    {
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly Mock<IApplicationRepository> _mockApplicationRepository;
        private readonly Mock<IConfiguration> _mockConfiguration;
        private readonly AuthenticationService _authenticationService;

        public AuthenticationServiceTests()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _mockApplicationRepository = new Mock<IApplicationRepository>();
            _mockConfiguration = new Mock<IConfiguration>();
            _authenticationService = new AuthenticationService(
                _mockUserRepository.Object,
                _mockApplicationRepository.Object,
                _mockConfiguration.Object
            );
        }

        [Fact]
        public async Task PostLogin_RequestWithValidationErrors_Returns422()
        {
            // Arrange
            var request = new PostLoginRequest
            {
                Email = "",
                Password = "pass",
                ApplicationName = "TestApp"
            };

            // Act
            var result = await _authenticationService.PostLogin(request);

            // Assert
            Assert.Equal(422, result.StatusCode);
            Assert.Contains(GlobalMessages.MANDATORY_EMAIL, result.Errors);
        }

        [Fact]
        public async Task PostLogin_UserNotFound_Returns404()
        {
            // Arrange
            var request = new PostLoginRequest
            {
                Email = "test@test.com",
                Password = "pass",
                ApplicationName = "TestApp"
            };

            _mockUserRepository.Setup(repo => repo.FindUserByEmail(It.IsAny<string>()))
                .ReturnsAsync((User)null);

            // Act
            var result = await _authenticationService.PostLogin(request);

            // Assert
            Assert.Equal(404, result.StatusCode);
            Assert.Contains(GlobalMessages.DATABASE_RECORD_NOT_FOUND, result.Errors);
        }

        [Fact]
        public async Task PostLogin_UserDisabled_Returns403()
        {
            // Arrange
            var request = new PostLoginRequest
            {
                Email = "test@test.com",
                Password = "pass",
                ApplicationName = "TestApp"
            };

            var user = new User { Enabled = false };

            _mockUserRepository.Setup(repo => repo.FindUserByEmail(It.IsAny<string>()))
                .ReturnsAsync(user);

            // Act
            var result = await _authenticationService.PostLogin(request);

            // Assert
            Assert.Equal(403, result.StatusCode);
            Assert.Contains(GlobalMessages.ACCOUNT_DISABLED, result.Errors);
        }    
    }
}
