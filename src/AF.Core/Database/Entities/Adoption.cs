namespace AF.Core.Database.Entities;

/// <summary>
/// It is required to fill in at least one source of information.
/// If the adopter is not a user of the platform, all information must be provided.
/// Otherwise, the necessary information can be found in the User entity.
/// </summary>
public class Adoption : BaseAuditableEntity
{
    public Guid AnimalId { get; set; }

    public Guid? AdopterId { get; set; }

    public string? FirstName { get; set; }
    
    public string? LastName { get; set; }
    
    public string? Address { get; set; }
    
    public string? Email { get; set; }
    
    public string? Phone { get; set; }

    public DateTime? AdoptionDate { get; set; }

    public AdoptionStatus AdoptionStatus { get; set; }
    
    public virtual Animal? Animal { get; set; }

    public virtual User? Adopter { get; set; }
}