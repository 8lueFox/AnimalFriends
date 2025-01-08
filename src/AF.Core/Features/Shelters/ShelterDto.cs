namespace AF.Core.Features.Shelters;

public class ShelterDto
{
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? BankAccount { get; set; }
    public string Owner { get; set; }
    public short CountOfAnimals { get; set; }
}