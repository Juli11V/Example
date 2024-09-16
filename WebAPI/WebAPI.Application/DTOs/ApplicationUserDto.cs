using Microsoft.AspNetCore.Identity;

namespace WebAPI.Application.DTOs;
public class ApplicationUserDto
{
    public string Id { get; set; }
    public string Username { get; set; }
    public string Role { get; set; }
}

