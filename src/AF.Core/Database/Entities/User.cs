namespace AF.Core.Database.Entities;

public class User : BaseAuditableEntity
{
    public string UserName { get; set; } = string.Empty;

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;
    
    public string Phone { get; set; } = string.Empty;
    
    public string Address { get; set; } = string.Empty;

    public Gender Gender { get; set; }

    public DateOnly Birthday { get; set; }

    public required byte[] HashedPassword { get; set; }

    
    public virtual IList<ShelterUser> Shelters { get; set; }
    
    public virtual IList<Animal> AssignedAnimals { get; set; }
    
    public virtual IList<Departure> Departures { get; set; }
    
    public virtual IList<Adoption> Adoptions { get; set; }
}