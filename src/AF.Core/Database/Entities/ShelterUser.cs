namespace AF.Core.Database.Entities;

public class ShelterUser : IHasId
{
    public Guid Id { get; }
    public Guid UserId { get; set; }
    
    public Guid ShelterId { get; set; }

    public DateOnly StarDate { get; set; }

    public bool IsOwner { get; set; }
    
    public bool IsAdmin { get; set; }

    public virtual User? User { get; set; }
    
    public virtual Shelter? Shelter { get; set; }
}