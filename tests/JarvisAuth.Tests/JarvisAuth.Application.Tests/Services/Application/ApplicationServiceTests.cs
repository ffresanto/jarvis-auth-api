using AutoMapper;
using JarvisAuth.Application.Services.Application;
using JarvisAuth.Core.Messages;
using JarvisAuth.Core.Requests.Application;
using JarvisAuth.Core.Responses.Application;
using JarvisAuth.Domain.Interfaces.Repositories.Application;
using JarvisAuth.Domain.Interfaces.Repositories.User;
using JarvisAuth.Domain.Models;
using Moq;

namespace JarvisAuth.Tests.JarvisAuth.Application.Tests.Services.Application
{
    public class ApplicationServiceTests
    {
        private readonly Mock<IApplicationRepository> _applicationRepositoryMock;
        private readonly Mock<IApplicationPermissionRepository> _applicationPermissionRepositoryMock;
        private readonly Mock<IUserPermissionRepository> _userPermissionRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly ApplicationService _applicationService;

        public ApplicationServiceTests()
        {
            _applicationRepositoryMock = new Mock<IApplicationRepository>();
            _applicationPermissionRepositoryMock = new Mock<IApplicationPermissionRepository>();
            _userPermissionRepositoryMock = new Mock<IUserPermissionRepository>();
            _mapperMock = new Mock<IMapper>();
            _applicationService = new ApplicationService(
                _applicationRepositoryMock.Object,
                _applicationPermissionRepositoryMock.Object,
                _userPermissionRepositoryMock.Object,
                _mapperMock.Object);
        }

        [Fact]
        public async Task PostApplication_ValidationFails_ReturnsErrorsAndStatus422()
        {
            // Arrange
            var request = new PostApplicationRequest
            {
                Name = ""
            };

            // Act
            var response = await _applicationService.PostApplication(request);

            // Assert
            Assert.NotNull(response.Errors);
            Assert.Equal(422, response.StatusCode);
        }

        [Fact]
        public async Task PostApplication_NameAlreadyExists_ReturnsErrorAndStatus409()
        {
            // Arrange
            var request = new PostApplicationRequest { Name = "Existing Application" };
            _applicationRepositoryMock
                .Setup(repo => repo.ApplicationNameExists(request.Name))
                .ReturnsAsync(true);

            // Act
            var response = await _applicationService.PostApplication(request);

            // Assert
            Assert.Contains(GlobalMessages.NAME_ALREADY_EXISTS, response.Errors);
            Assert.Equal(409, response.StatusCode);
        }

        [Fact]
        public async Task PostApplication_SaveChangesFails_ReturnsErrorAndStatus500()
        {
            // Arrange
            var request = new PostApplicationRequest { Name = "New Application" };
            _applicationRepositoryMock
                .Setup(repo => repo.ApplicationNameExists(request.Name))
                .ReturnsAsync(false);
            _mapperMock
                .Setup(m => m.Map<Domain.Entities.Application>(request))
                .Returns(new Domain.Entities.Application { Id = new Guid() });
            _applicationRepositoryMock
                .Setup(repo => repo.CreateApplication(It.IsAny<Domain.Entities.Application>()));
            _applicationRepositoryMock
                .Setup(repo => repo.SaveChangesAsync())
                .ReturnsAsync(false);

            // Act
            var response = await _applicationService.PostApplication(request);

            // Assert
            Assert.Contains(GlobalMessages.DATABASE_SAVE_FAILED, response.Errors);
            Assert.Equal(500, response.StatusCode);
        }

        [Fact]
        public async Task PostApplication_Success_ReturnsApplicationIdAndStatus200()
        {
            // Arrange
            var id = new Guid();
            var request = new PostApplicationRequest { Name = "New Application" };
            _applicationRepositoryMock
                .Setup(repo => repo.ApplicationNameExists(request.Name))
                .ReturnsAsync(false);
            _mapperMock
                .Setup(m => m.Map<Domain.Entities.Application>(request))
                .Returns(new Domain.Entities.Application { Id = id });
            _applicationRepositoryMock
                .Setup(repo => repo.CreateApplication(It.IsAny<Domain.Entities.Application>()));
            _applicationRepositoryMock
                .Setup(repo => repo.SaveChangesAsync())
                .ReturnsAsync(true);

            // Act
            var response = await _applicationService.PostApplication(request);

            // Assert
            Assert.NotNull(response.Data);
            Assert.Equal(id, response.Data.ApplicationId);
            Assert.Equal(200, response.StatusCode);
        }

        [Fact]
        public async Task GetApplications_NoRecordsFound_ReturnsErrorAndStatus404()
        {
            // Arrange
            _applicationRepositoryMock
                .Setup(repo => repo.GetAllApplications())
                .ReturnsAsync((List<Domain.Entities.Application>)null);

            // Act
            var response = await _applicationService.GetApplications();

            // Assert
            Assert.NotNull(response.Errors);
            Assert.Contains(GlobalMessages.DATABASE_RECORD_NOT_FOUND, response.Errors);
            Assert.Equal(404, response.StatusCode);
        }

        [Fact]
        public async Task GetApplications_ApplicationsReturned_ReturnsApplicationsAndStatus200()
        {
            // Arrange
            var idApplication1 = new Guid();
            var idApplication2 = new Guid();

            var applications = new List<Domain.Entities.Application>
        {
            new Domain.Entities.Application { Id = idApplication1, Name = "Application 1" },
            new Domain.Entities.Application { Id = idApplication2, Name = "Application 2" }
        };

            _applicationRepositoryMock
                .Setup(repo => repo.GetAllApplications())
                .ReturnsAsync(applications);

            var expectedResponses = new List<GetApplicationResponse>
        {
            new GetApplicationResponse { Id = idApplication1, Name = "Application 1" },
            new GetApplicationResponse { Id = idApplication2, Name = "Application 2" }
        };

            _mapperMock
                .Setup(m => m.Map<List<GetApplicationResponse>>(applications))
                .Returns(expectedResponses);

            // Act
            var response = await _applicationService.GetApplications();

            // Assert
            Assert.NotNull(response.Data);
            Assert.Equal(2, response.Data.Count);
            Assert.Equal(expectedResponses, response.Data);
            Assert.Equal(200, response.StatusCode);
        }

        [Fact]
        public async Task PostApplicationPermission_ValidationFails_ReturnsErrorsAndStatus422()
        {
            // Arrange
            var request = new PostApplicationPermissionRequest
            {
                Name = ""
            };

            // Act
            var response = await _applicationService.PostApplicationPermission(request);

            // Assert
            Assert.NotNull(response.Errors);
            Assert.Equal(422, response.StatusCode);
        }

        [Fact]
        public async Task PostApplicationPermission_NameAlreadyExists_ReturnsErrorAndStatus409()
        {
            // Arrange
            var request = new PostApplicationPermissionRequest { Name = "Existing Permission", ApplicationId = new Guid() };
            _applicationPermissionRepositoryMock
                .Setup(repo => repo.ApplicationPermissionNameExists(request.Name))
                .ReturnsAsync(true);

            // Act
            var response = await _applicationService.PostApplicationPermission(request);

            // Assert
            Assert.Contains(GlobalMessages.NAME_ALREADY_EXISTS, response.Errors);
            Assert.Equal(409, response.StatusCode);
        }

        [Fact]
        public async Task PostApplicationPermission_ApplicationIdNotExists_ReturnsErrorAndStatus409()
        {
            // Arrange
            var request = new PostApplicationPermissionRequest { Name = "New Permission", ApplicationId = new Guid() };
            _applicationPermissionRepositoryMock
                .Setup(repo => repo.ApplicationPermissionNameExists(request.Name))
                .ReturnsAsync(false);
            _applicationRepositoryMock
                .Setup(repo => repo.ApplicationIdExists(request.ApplicationId))
                .ReturnsAsync(false);

            // Act
            var response = await _applicationService.PostApplicationPermission(request);

            // Assert
            Assert.Contains(GlobalMessages.APPLICATION_NOT_EXISTS, response.Errors);
            Assert.Equal(409, response.StatusCode);
        }

        [Fact]
        public async Task GetFindApplicationWithPermissions_NoApplicationIdAndPermissionName_ReturnsErrorsAndStatus422()
        {
            // Arrange
            Guid? applicationId = null;
            string permissionName = null;

            // Act
            var response = await _applicationService.GetFindApplicationWithPermissions(applicationId, permissionName);

            // Assert
            Assert.NotNull(response.Errors);
            Assert.Contains(GlobalMessages.PROVIDER_APPLICATION_AND_PERMISSION, response.Errors);
            Assert.Equal(422, response.StatusCode);
        }

        [Fact]
        public async Task GetFindApplicationWithPermissions_NoDataFound_ReturnsErrorAndStatus404()
        {
            // Arrange
            Guid? applicationId = Guid.NewGuid();
            string permissionName = "SomePermission";
            _applicationRepositoryMock
                .Setup(repo => repo.FindApplicationWithListPermissions(applicationId, permissionName))
                .ReturnsAsync((List<ApplicationPermissionData>)null);

            // Act
            var response = await _applicationService.GetFindApplicationWithPermissions(applicationId, permissionName);

            // Assert
            Assert.Contains(GlobalMessages.DATABASE_RECORD_NOT_FOUND, response.Errors);
            Assert.Equal(404, response.StatusCode);
        }

        [Fact]
        public async Task GetFindApplicationWithPermissions_Success_ReturnsMappedDataAndStatus200()
        {
            // Arrange
            var applicationId = Guid.NewGuid();
            var permissionName = "SomePermission";
            var mockData = new List<ApplicationPermissionData>
        {
            new ApplicationPermissionData { Id = applicationId, Permission = permissionName }
        };

            _applicationRepositoryMock
                .Setup(repo => repo.FindApplicationWithListPermissions(applicationId, permissionName))
                .ReturnsAsync(mockData);

            _mapperMock
                .Setup(m => m.Map<List<GetApplicationWithPermissionsResponse>>(mockData))
                .Returns(new List<GetApplicationWithPermissionsResponse>
                {
                new GetApplicationWithPermissionsResponse { Id = applicationId, Permission = permissionName }
                });

            // Act
            var response = await _applicationService.GetFindApplicationWithPermissions(applicationId, permissionName);

            // Assert
            Assert.NotNull(response.Data);
            Assert.Single(response.Data);
            Assert.Equal(applicationId, response.Data[0].Id);
            Assert.Equal(permissionName, response.Data[0].Permission);
            Assert.Equal(200, response.StatusCode);
        }

        [Fact]
        public async Task PatchToggleEnabled_ApplicationNotFound_ReturnsErrorAndStatus409()
        {
            // Arrange
            var request = new PatchApplicationToggleEnabledRequest
            {
                ApplicationId = Guid.NewGuid(),
                Enable = true
            };

            _applicationRepositoryMock
                .Setup(repo => repo.FindApplicationById(request.ApplicationId))
                .ReturnsAsync((Domain.Entities.Application)null);

            // Act
            var response = await _applicationService.PatchToggleEnabled(request);

            // Assert
            Assert.Contains(GlobalMessages.USER_NOT_EXISTS, response.Errors);
            Assert.Equal(409, response.StatusCode);
        }

        [Fact]
        public async Task PatchToggleEnabled_DatabaseSaveError_ReturnsErrorAndStatus500()
        {
            // Arrange
            var request = new PatchApplicationToggleEnabledRequest
            {
                ApplicationId = Guid.NewGuid(),
                Enable = true
            };

            var mockApplication = new Domain.Entities.Application
            {
                Id = request.ApplicationId,
                Enabled = false
            };

            _applicationRepositoryMock
                .Setup(repo => repo.FindApplicationById(request.ApplicationId))
                .ReturnsAsync(mockApplication);

            _applicationRepositoryMock
                .Setup(repo => repo.SaveChangesAsync())
                .ReturnsAsync(false);

            // Act
            var response = await _applicationService.PatchToggleEnabled(request);

            // Assert
            Assert.Contains(GlobalMessages.DATABASE_SAVE_FAILED, response.Errors);
            Assert.Equal(500, response.StatusCode);
        }

        [Fact]
        public async Task PatchToggleEnabled_Success_ReturnsSuccessMessageAndStatus200()
        {
            // Arrange
            var request = new PatchApplicationToggleEnabledRequest
            {
                ApplicationId = Guid.NewGuid(),
                Enable = true
            };

            var mockApplication = new Domain.Entities.Application
            {
                Id = request.ApplicationId,
                Enabled = false
            };

            _applicationRepositoryMock
                .Setup(repo => repo.FindApplicationById(request.ApplicationId))
                .ReturnsAsync(mockApplication);

            _applicationRepositoryMock
                .Setup(repo => repo.SaveChangesAsync())
                .ReturnsAsync(true);

            // Act
            var response = await _applicationService.PatchToggleEnabled(request);

            // Assert
            Assert.NotNull(response.Data);
            Assert.Equal(GlobalMessages.RECORD_UPDATED_SUCCESSFULLY, response.Data.Info);
            Assert.Equal(200, response.StatusCode);
        }

        [Fact]
        public async Task DeleteApplicationPermission_PermissionAssociatedWithUser_ReturnsErrorAndStatus409()
        {
            // Arrange
            var request = new DeleteApplicationPermissionRequest
            {
                PermissionId = Guid.NewGuid()
            };

            _userPermissionRepositoryMock
                .Setup(repo => repo.UserPermissionExistsById(request.PermissionId))
                .ReturnsAsync(true);

            // Act
            var response = await _applicationService.DeleteApplicationPermission(request);

            // Assert
            Assert.Contains(GlobalMessages.PERMISSION_ASSOCIATED_USER, response.Errors);
            Assert.Equal(409, response.StatusCode);
        }

        [Fact]
        public async Task DeleteApplicationPermission_PermissionNotFound_ReturnsErrorAndStatus409()
        {
            // Arrange
            var request = new DeleteApplicationPermissionRequest
            {
                PermissionId = Guid.NewGuid()
            };

            _userPermissionRepositoryMock
                .Setup(repo => repo.UserPermissionExistsById(request.PermissionId))
                .ReturnsAsync(false);

            _applicationPermissionRepositoryMock
                .Setup(repo => repo.DeleteApplicationPermission(request.PermissionId))
                .ReturnsAsync(false);

            // Act
            var response = await _applicationService.DeleteApplicationPermission(request);

            // Assert
            Assert.Contains(GlobalMessages.PERMISSION_NOT_EXISTS, response.Errors);
            Assert.Equal(409, response.StatusCode);
        }

        [Fact]
        public async Task DeleteApplicationPermission_DatabaseDeleteError_ReturnsErrorAndStatus500()
        {
            // Arrange
            var request = new DeleteApplicationPermissionRequest
            {
                PermissionId = Guid.NewGuid()
            };

            _userPermissionRepositoryMock
                .Setup(repo => repo.UserPermissionExistsById(request.PermissionId))
                .ReturnsAsync(false);

            _applicationPermissionRepositoryMock
                .Setup(repo => repo.DeleteApplicationPermission(request.PermissionId))
                .ReturnsAsync(true);

            _applicationPermissionRepositoryMock
                .Setup(repo => repo.SaveChangesAsync())
                .ReturnsAsync(false);

            // Act
            var response = await _applicationService.DeleteApplicationPermission(request);

            // Assert
            Assert.Contains(GlobalMessages.DATABASE_DELETE_FAILED, response.Errors);
            Assert.Equal(500, response.StatusCode);
        }

        [Fact]
        public async Task DeleteApplicationPermission_Success_ReturnsSuccessMessageAndStatus200()
        {
            // Arrange
            var request = new DeleteApplicationPermissionRequest
            {
                PermissionId = Guid.NewGuid()
            };

            _userPermissionRepositoryMock
                .Setup(repo => repo.UserPermissionExistsById(request.PermissionId))
                .ReturnsAsync(false);

            _applicationPermissionRepositoryMock
                .Setup(repo => repo.DeleteApplicationPermission(request.PermissionId))
                .ReturnsAsync(true);

            _applicationPermissionRepositoryMock
                .Setup(repo => repo.SaveChangesAsync())
                .ReturnsAsync(true);

            // Act
            var response = await _applicationService.DeleteApplicationPermission(request);

            // Assert
            Assert.NotNull(response.Data);
            Assert.Equal(GlobalMessages.RECORD_DELETE_SUCCESSFULLY, response.Data.Info);
            Assert.Equal(200, response.StatusCode);
        }

        [Fact]
        public async Task PatchApplication_ApplicationNameMissing_ReturnsErrorAndStatus422()
        {
            // Arrange
            var request = new PatchApplicationRequest
            {
                ApplicationId = Guid.NewGuid(),
                Name = string.Empty // Empty Name
            };

            // Act
            var response = await _applicationService.PatchApplication(request);

            // Assert
            Assert.Contains(GlobalMessages.MANDATORY_APPLICATION_NAME, response.Errors);
            Assert.Equal(422, response.StatusCode);
        }

        [Fact]
        public async Task PatchApplication_ApplicationNotFound_ReturnsErrorAndStatus409()
        {
            // Arrange
            var request = new PatchApplicationRequest
            {
                ApplicationId = Guid.NewGuid(),
                Name = "New Application Name"
            };

            _applicationRepositoryMock
                .Setup(repo => repo.FindApplicationById(request.ApplicationId))
                .ReturnsAsync((Domain.Entities.Application)null); // Application not found

            // Act
            var response = await _applicationService.PatchApplication(request);

            // Assert
            Assert.Contains(GlobalMessages.APPLICATION_NOT_EXISTS, response.Errors);
            Assert.Equal(409, response.StatusCode);
        }

        [Fact]
        public async Task PatchApplication_DatabaseSaveError_ReturnsErrorAndStatus500()
        {
            // Arrange
            var request = new PatchApplicationRequest
            {
                ApplicationId = Guid.NewGuid(),
                Name = "Updated Application Name"
            };

            var application = new Domain.Entities.Application
            {
                Id = request.ApplicationId,
                Name = "Old Application Name",
                UpdatedAt = DateTime.UtcNow
            };

            _applicationRepositoryMock
                .Setup(repo => repo.FindApplicationById(request.ApplicationId))
                .ReturnsAsync(application); // Application found

            _applicationRepositoryMock
                .Setup(repo => repo.SaveChangesAsync())
                .ReturnsAsync(false); // Simulate failure to save changes

            // Act
            var response = await _applicationService.PatchApplication(request);

            // Assert
            Assert.Contains(GlobalMessages.DATABASE_SAVE_FAILED, response.Errors);
            Assert.Equal(500, response.StatusCode);
        }

        [Fact]
        public async Task PatchApplication_Success_ReturnsSuccessMessageAndStatus200()
        {
            // Arrange
            var request = new PatchApplicationRequest
            {
                ApplicationId = Guid.NewGuid(),
                Name = "Updated Application Name"
            };

            var application = new Domain.Entities.Application
            {
                Id = request.ApplicationId,
                Name = "Old Application Name",
                UpdatedAt = DateTime.UtcNow
            };

            _applicationRepositoryMock
                .Setup(repo => repo.FindApplicationById(request.ApplicationId))
                .ReturnsAsync(application); // Application found

            _applicationRepositoryMock
                .Setup(repo => repo.SaveChangesAsync())
                .ReturnsAsync(true); // Simulate successful save

            // Act
            var response = await _applicationService.PatchApplication(request);

            // Assert
            Assert.NotNull(response.Data);
            Assert.Equal(GlobalMessages.RECORD_UPDATED_SUCCESSFULLY, response.Data.Info);
            Assert.Equal(200, response.StatusCode);
        }
    }
}

