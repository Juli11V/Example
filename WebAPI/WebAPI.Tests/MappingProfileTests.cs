using AutoMapper;
using FluentAssertions;
using WebAPI.Application.DTOs;
using WebAPI.Application.Services;
using WebAPI.Domain.Entities;
using Xunit;
namespace WebAPI.Tests;
public class MappingProfileTests
{
    private readonly IMapper _mapper;

    public MappingProfileTests()
    {
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
        _mapper = new Mapper(configuration);
    }
    [Theory]
    [InlineData("testuser", "password123")]
    [InlineData("anotheruser", "differentpassword")]
    public void MappingProfile_MapRegisterRequestToApplicationUser_IsValid(string username, string password)
    {
        var registerRequest = new RegisterRequest { Username = username, Password = password };

        var mappedUser = _mapper.Map<ApplicationUser>(registerRequest);

        mappedUser.Should().NotBeNull();
        mappedUser.UserName.Should().Be(registerRequest.Username);
        mappedUser.Email.Should().BeNull();
    }
}

