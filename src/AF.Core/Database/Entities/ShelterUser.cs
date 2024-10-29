namespace AF.Core.Database.Entities;

public class ShelterUser : BaseAuditableEntity
{
    public Guid UserId { get; set; }
    
    public Guid ShelterId { get; set; }

    public DateTime StarDate { get; set; }

    public bool IsOwner { get; set; }
    
    public bool IsAdmin { get; set; }

    public virtual User? User { get; set; }
    
    public virtual Shelter? Shelter { get; set; }
}