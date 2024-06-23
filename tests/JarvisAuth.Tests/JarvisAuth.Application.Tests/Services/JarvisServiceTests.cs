using AutoMapper;
using JarvisAuth.Application.Services;
using JarvisAuth.Core.Messages;
using JarvisAuth.Core.Requests.Jarvis;
using JarvisAuth.Core.Responses.Jarvis;
using JarvisAuth.Core.Responses.Shared;
using JarvisAuth.Domain.Entities;
using JarvisAuth.Domain.Interfaces.Repositories;
using JarvisAuth.Domain.models;
using Moq;

namespace JarvisAuth.Tests.JarvisAuth.Application.Tests.Services
{
    public class JarvisServiceTests
    {
        private readonly Mock<IJarvisRepository> _jarvisRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly JarvisService _jarvisService;

        public JarvisServiceTests()
        {
            _jarvisRepositoryMock = new Mock<IJarvisRepository>();
            _mapperMock = new Mock<IMapper>();
            _jarvisService = new JarvisService(_jarvisRepositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task GetGendersTypes_ReturnsData_WhenDataExists()
        {
            // Arrange
            var genderTypes = new List<GenderType> { new GenderType() };
            var mappedResponse = new List<GetGenderTypeResponse> { new GetGenderTypeResponse() };
            _jarvisRepositoryMock.Setup(repo => repo.GetGenderTypes()).ReturnsAsync(genderTypes);
            _mapperMock.Setup(mapper => mapper.Map<List<GetGenderTypeResponse>>(genderTypes)).Returns(mappedResponse);

            // Act
            var result = await _jarvisService.GetGendersTypes();

            // Assert
            Assert.NotNull(result.Data);
            Assert.Equal(mappedResponse, result.Data);
            Assert.Empty(result.Errors);
        }

        [Fact]
        public async Task GetGendersTypes_ReturnsNotFound_WhenDataDoesNotExist()
        {
            // Arrange
            _jarvisRepositoryMock.Setup(repo => repo.GetGenderTypes()).ReturnsAsync((List<GenderType>)null);

            // Act
            var result = await _jarvisService.GetGendersTypes();

            // Assert
            Assert.Null(result.Data);
            Assert.Contains(GlobalMessages.RECORDS_NOT_FOUND_IN_DATABASE, result.Errors);
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public async Task GetDocumentsTypes_ReturnsData_WhenDataExists()
        {
            // Arrange
            var documentTypes = new List<DocumentType> { new DocumentType() };
            var mappedResponse = new List<GetDocumentTypeResponse> { new GetDocumentTypeResponse() };
            _jarvisRepositoryMock.Setup(repo => repo.GetDocumentTypes()).ReturnsAsync(documentTypes);
            _mapperMock.Setup(mapper => mapper.Map<List<GetDocumentTypeResponse>>(documentTypes)).Returns(mappedResponse);

            // Act
            var result = await _jarvisService.GetDocumentsTypes();

            // Assert
            Assert.NotNull(result.Data);
            Assert.Equal(mappedResponse, result.Data);
            Assert.Empty(result.Errors);
        }

        [Fact]
        public async Task GetDocumentsTypes_ReturnsNotFound_WhenDataDoesNotExist()
        {
            // Arrange
            _jarvisRepositoryMock.Setup(repo => repo.GetDocumentTypes()).ReturnsAsync((List<DocumentType>)null);

            // Act
            var result = await _jarvisService.GetDocumentsTypes();

            // Assert
            Assert.Null(result.Data);
            Assert.Contains(GlobalMessages.RECORDS_NOT_FOUND_IN_DATABASE, result.Errors);
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public async Task PostCreateUserJarvis_ReturnsError_WhenEmailExists()
        {
            // Arrange
            var request = new PostCreateUserJarvisRequest
            {
                Name = "John Doe",
                Email = "test@example.com",
                Password = "password123",
                ContactNumber = "1234567890",
                GenderTypeId = 1,
                DocumentTypeId = 1,
                DocumentNumber = "123456",
            };

            var response = new Response<PostCreateUserJarvisResponse>();

            _jarvisRepositoryMock.Setup(repo => repo.EmailExistsAsync(request.Email)).ReturnsAsync(true);

            // Act
            var result = await _jarvisService.PostCreateUserJarvis(request);

            // Assert
            Assert.NotNull(result.Errors);
            Assert.Contains("Email already exists", result.Errors);
            Assert.Equal(409, result.StatusCode);
        }

    }
}
