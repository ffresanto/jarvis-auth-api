using AutoMapper;
using JarvisAuth.Application.Services;
using JarvisAuth.Core.Messages;
using JarvisAuth.Core.Responses.Types;
using JarvisAuth.Domain.Interfaces.Repositories;
using JarvisAuth.Domain.models;
using Moq;

namespace JarvisAuth.Tests.JarvisAuth.Application.Tests.Services
{
    public class SystemServiceTests
    {
        private readonly Mock<ISystemRepository> _typesRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly SystemService _typeService;

        public SystemServiceTests()
        {
            _typesRepositoryMock = new Mock<ISystemRepository>();
            _mapperMock = new Mock<IMapper>();
            _typeService = new SystemService(_typesRepositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task GetGendersTypes_ReturnsData_WhenDataExists()
        {
            // Arrange
            var genderTypes = new List<GenderType> { new GenderType() };
            var mappedResponse = new List<GetGenderTypeResponse> { new GetGenderTypeResponse() };
            _typesRepositoryMock.Setup(repo => repo.GetGenderTypes()).ReturnsAsync(genderTypes);
            _mapperMock.Setup(mapper => mapper.Map<List<GetGenderTypeResponse>>(genderTypes)).Returns(mappedResponse);

            // Act
            var result = await _typeService.GetGendersTypes();

            // Assert
            Assert.NotNull(result.Data);
            Assert.Equal(mappedResponse, result.Data);
            Assert.Empty(result.Errors);
        }

        [Fact]
        public async Task GetGendersTypes_ReturnsNotFound_WhenDataDoesNotExist()
        {
            // Arrange
            _typesRepositoryMock.Setup(repo => repo.GetGenderTypes()).ReturnsAsync((List<GenderType>)null);

            // Act
            var result = await _typeService.GetGendersTypes();

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
            _typesRepositoryMock.Setup(repo => repo.GetDocumentTypes()).ReturnsAsync(documentTypes);
            _mapperMock.Setup(mapper => mapper.Map<List<GetDocumentTypeResponse>>(documentTypes)).Returns(mappedResponse);

            // Act
            var result = await _typeService.GetDocumentsTypes();

            // Assert
            Assert.NotNull(result.Data);
            Assert.Equal(mappedResponse, result.Data);
            Assert.Empty(result.Errors);
        }

        [Fact]
        public async Task GetDocumentsTypes_ReturnsNotFound_WhenDataDoesNotExist()
        {
            // Arrange
            _typesRepositoryMock.Setup(repo => repo.GetDocumentTypes()).ReturnsAsync((List<DocumentType>)null);

            // Act
            var result = await _typeService.GetDocumentsTypes();

            // Assert
            Assert.Null(result.Data);
            Assert.Contains(GlobalMessages.RECORDS_NOT_FOUND_IN_DATABASE, result.Errors);
            Assert.Equal(404, result.StatusCode);
        }
    }
}
