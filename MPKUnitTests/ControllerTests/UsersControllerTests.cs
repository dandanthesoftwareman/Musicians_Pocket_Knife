using System.Threading.Tasks;
using Musicians_Pocket_Knife.Controllers;
using Moq;
using Musicians_Pocket_Knife.Orchestrators;
using AutoMapper;
using Musicians_Pocket_Knife.Models;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using System;

namespace MPKUnitTests.ControllerTests
{
    public class UsersControllerTests
    {
        private readonly UsersController _usersController;
        private readonly Mock<IUserOrchestrator> _mockUserOrchestrator;
        private readonly Mock<IMapper> _mapper;

        public UsersControllerTests()
        {
            _mockUserOrchestrator = new Mock<IUserOrchestrator>();
            _mapper = new Mock<IMapper>();
            _usersController = new UsersController(_mockUserOrchestrator.Object, _mapper.Object);
        }

        [Fact]
        public async Task CreateNewUser_GivenValidUserInformation_ReturnsOk()
        {
            // Arrange
            var request = new CreateNewUserRequest
            {
                Id = "12345",
                FirstName = "Dan",
                LastName = "Schuler",
                Email = "dan@dan.dan"
            };

            var expectedUserResponse = new User
            {
                FirstName = "Dan",
                LastName = "Schuler"
            };

            _mapper.Setup(x => x.Map<User>(request)).Returns(It.IsAny<User?>);
            _mockUserOrchestrator.Setup(x => x.CreateNewUserAsync(It.IsAny<User?>())).ReturnsAsync(expectedUserResponse);

            // Act
            var result = await _usersController.CreateNewUser(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var actualUser = Assert.IsType<User>(okResult.Value);
            Assert.Equal(expectedUserResponse.FirstName, actualUser.FirstName);
            Assert.Equal(expectedUserResponse.LastName, actualUser.LastName);
            Assert.Null(actualUser.GoogleId);
        }

        [Fact]
        public async Task CreateNewUser_GivenInValidUserInformation_MappingFailsAndReturnsBadRequest()
        {
            // Arrange
            var request = new CreateNewUserRequest();

            _mapper.Setup(x => x.Map<User>(It.IsAny<CreateNewUserRequest>())).Throws(new Exception("Mapping failed"));

            // Act
            var result = await _usersController.CreateNewUser(request);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
