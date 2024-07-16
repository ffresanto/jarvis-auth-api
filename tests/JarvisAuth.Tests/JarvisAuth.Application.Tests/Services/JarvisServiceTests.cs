using AutoMapper;
using JarvisAuth.Application.Services;
using JarvisAuth.Core.Messages;
using JarvisAuth.Core.Requests.Jarvis;
using JarvisAuth.Core.Responses.Jarvis;
using JarvisAuth.Core.Responses.Shared;
using JarvisAuth.Domain.Interfaces.Repositories;
using Microsoft.Extensions.Configuration;
using Moq;

namespace JarvisAuth.Tests.JarvisAuth.Application.Tests.Services
{
    public class JarvisServiceTests
    {
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly Mock<IJarvisRepository> _jarvisRepositoryMock;
        private readonly Mock<IUserJarvisLinkedApplicationRepository> _userJarvisProfileApplicationRepository;
        private readonly Mock<IApplicationRepository> _applicationRepository;
        private readonly Mock<IMapper> _mapperMock;
        private readonly JarvisService _jarvisService;

        public JarvisServiceTests()
        {
            _configurationMock = new Mock<IConfiguration>();
            _jarvisRepositoryMock = new Mock<IJarvisRepository>();
            _userJarvisProfileApplicationRepository = new Mock<IUserJarvisLinkedApplicationRepository>();
            _applicationRepository = new Mock<IApplicationRepository>();
            _mapperMock = new Mock<IMapper>();
            _jarvisService = new JarvisService(_configurationMock.Object, _jarvisRepositoryMock.Object, _userJarvisProfileApplicationRepository.Object, _applicationRepository.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task PostCreateUserJarvis_ReturnsError_WhenEmailExists()
        {
            // Arrange
            var request = new PostUserJarvisRequest
            {
                Name = "John Doe",
                Email = "test@example.com",
                Password = "password123"
            };

            var response = new Response<PostUserJarvisResponse>();

            _jarvisRepositoryMock.Setup(repo => repo.UserEmailExists(request.Email)).ReturnsAsync(true);

            // Act
            var result = await _jarvisService.PostUserJarvis(request);

            // Assert
            Assert.NotNull(result.Errors);
            Assert.Contains(GlobalMessages.EMAIL_ALREADY_EXISTS, result.Errors);
            Assert.Equal(409, result.StatusCode);
        }
    }
}
