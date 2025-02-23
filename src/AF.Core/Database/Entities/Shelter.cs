namespace AF.Core.Database.Entities;

public class Shelter : BaseAuditableEntity
{
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? BankAccount { get; set; }
    
    public virtual IList<ShelterUser> Users { get; set; }
    public virtual IList<Animal> Animals { get; set; }
}