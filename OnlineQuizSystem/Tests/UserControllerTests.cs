using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using OnlineQuizSystemApi.Controllers;
using OnlineQuizSystemApi.DTOs;
using OnlineQuizSystemApi.Models;

namespace Tests
{
    public class UsersControllerTests
    {
        private readonly Mock<OnlineQuizSystemContext> _contextMock;
        private readonly UsersController _controller;

        public UsersControllerTests()
        {
            _contextMock = new Mock<OnlineQuizSystemContext>();
            _controller = new UsersController(_contextMock.Object);
        }

        [Fact]
        public async Task Register_ShouldReturnBadRequest_WhenEmailExists()
        {
            // Arrange
            var registerDto = new RegisterDto
            {
                Email = "existing@example.com",
                Password = "Password123!",
                FirstName = "John",
                LastName = "Doe",
                RoleId = 1
            };

            var users = new List<User>
            {
                new User
                {
                    Email = "existing@example.com",
                    PwdHash = "hashedPassword",
                    PwdSalt = "salt",
                    FirstName = "John",
                    LastName = "Doe",
                    RoleId = 1
                }
            };

            // Use Moq.EntityFrameworkCore for mocking DbSet<User>
            _contextMock.Setup(c => c.Users).ReturnsDbSet(users);

            // Act
            var result = await _controller.Register(registerDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Email is already registered.", badRequestResult.Value);
        }


        [Fact]
        public async Task Register_ShouldReturnOk_WhenUserIsRegistered()
        {
            // Arrange
            var registerDto = new RegisterDto
            {
                Email = "newuser@example.com",
                Password = "Password123!",
                FirstName = "Jane",
                LastName = "Doe",
                RoleId = 1
            };

            var users = new List<User>(); // Empty list simulates no matching email
            var roles = new List<Role>
            {
                new Role { Id = 1, Name = "Admin" }
            };

            // Mock Users and Roles DbSets
            _contextMock.Setup(c => c.Users).ReturnsDbSet(users);
            _contextMock.Setup(c => c.Roles).ReturnsDbSet(roles);

            // Act
            var result = await _controller.Register(registerDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("User registered successfully!", okResult.Value);
        }


        [Fact]
        public async Task Login_ShouldReturnUnauthorized_WhenEmailIsInvalid()
        {
            // Arrange
            var loginDto = new LoginDto
            {
                Email = "invalid@example.com",
                Password = "Password123!"
            };

            var users = new List<User> // Simulate an empty DbSet
            {
                new User
                {
                    Email = "valid@example.com",
                    PwdHash = "hashedPassword",
                    PwdSalt = "salt",
                    FirstName = "Valid",
                    LastName = "User",
                    RoleId = 1
                }
            };

            _contextMock.Setup(c => c.Users).ReturnsDbSet(users);

            var controller = new UsersController(_contextMock.Object);

            // Act
            var result = await controller.Login(loginDto);

            // Assert
            var unauthorizedResult = Assert.IsType<UnauthorizedObjectResult>(result);
            Assert.Equal("Invalid email or password.", unauthorizedResult.Value);
        }

    }
}
