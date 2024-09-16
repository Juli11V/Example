namespace WebAPI.Application.DTOs;
public record RegisterRequest
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
}

