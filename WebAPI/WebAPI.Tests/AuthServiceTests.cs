using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Moq;
using WebAPI.Application.DTOs;
using WebAPI.Application.Interfaces;
using WebAPI.Application.Services;
using WebAPI.Domain.Entities;
using Xunit;

namespace WebAPI.Tests
{
    public class AuthServiceTests
    {
        private readonly Mock<IUserManagerDecorator<ApplicationUser>> _userManagerMock;
        private readonly Mock<ISignInManagerDecorator<ApplicationUser>> _signInManagerMock;
        private readonly Mock<ITokenService> _tokenServiceMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly AuthService _authService;

        public AuthServiceTests()
        {
            _userManagerMock = new Mock<IUserManagerDecorator<ApplicationUser>>();
            _signInManagerMock = new Mock<ISignInManagerDecorator<ApplicationUser>>();
            _tokenServiceMock = new Mock<ITokenService>();
            _mapperMock = new Mock<IMapper>();
            _authService = new AuthService(_userManagerMock.Object, _signInManagerMock.Object, _tokenServiceMock.Object, _mapperMock.Object);

            SetupMocks();
        }

        private void SetupMocks()
        {
            var user = new ApplicationUser { UserName = "username" };

            _userManagerMock.Setup(x => x.FindByNameAsync("username")).ReturnsAsync(user);
            _signInManagerMock.Setup(x => x.PasswordSignInAsync("username", "password", false, false)).ReturnsAsync(SignInResult.Success);
            _tokenServiceMock.Setup(x => x.GenerateJwtToken(user)).ReturnsAsync("fake_jwt_token");
        }

        [Fact]
        public async Task LoginAsync_UserSuccessfullyLoggedIn_ReturnsJwtToken()
        {
            var loginRequest = new LoginRequest
            {
                Username = "username",
                Password = "password"
            };

            var result = await _authService.LoginAsync(loginRequest);

            result.Should().NotBeNull();
            result.Should().Be("fake_jwt_token");
        }

        [Fact]
        public async Task LoginAsync_UnsuccessfulLogin_ReturnsNullToken()
        {
            var loginRequest = new LoginRequest
            {
                Username = "username",
                Password = "incorrect_password"
            };

            _signInManagerMock.Setup(x => x.PasswordSignInAsync(loginRequest.Username, loginRequest.Password, false, false))
                .ReturnsAsync(SignInResult.Failed);

            var result = await _authService.LoginAsync(loginRequest);

            result.Should().BeNull();
        }

        [Fact]
        public async Task LoginAsync_ShouldFindUserByUsername()
        {
            var loginRequest = new LoginRequest
            {
                Username = "username",
                Password = "password"
            };

            await _authService.LoginAsync(loginRequest);

            _userManagerMock.Verify(x => x.FindByNameAsync("username"), Times.Once);
        }

        [Fact]
        public async Task LoginAsync_ShouldSignInUsingSignInManager()
        {
            var loginRequest = new LoginRequest
            {
                Username = "username",
                Password = "password"
            };

            await _authService.LoginAsync(loginRequest);

            _signInManagerMock.Verify(x => x.PasswordSignInAsync("username", "password", false, false), Times.Once);
        }

        [Fact]
        public async Task LoginAsync_ShouldUseJwtTokenServiceToGenerateToken()
        {
            var loginRequest = new LoginRequest
            {
                Username = "username",
                Password = "password"
            };

            var user = new ApplicationUser { UserName = "username" };
            _userManagerMock.Setup(x => x.FindByNameAsync("username")).ReturnsAsync(user);

            await _authService.LoginAsync(loginRequest);

            _tokenServiceMock.Verify(x => x.GenerateJwtToken(user), Times.Once);
        }
    }
}