namespace AF.Core.Features.Users;

public sealed class UserDto
{
    public string UserName { get; set; } = string.Empty;

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;
    
    public string Phone { get; set; } = string.Empty;
    
    public string Address { get; set; } = string.Empty;

    public Gender Gender { get; set; }

    public DateOnly Birthday { get; set; }
}