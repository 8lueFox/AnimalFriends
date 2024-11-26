namespace AF.Core.Database.Entities;

public class Animal : BaseAuditableEntity, IHasIdWithName
{
    public Guid ShelterId { get; set; }
    public Guid? UserId { get; set; }
    
    public string Name { get; set; } = string.Empty;

    public Gender Gender { get; set; }
    
    /// <summary>
    /// E.g. dog, cat, hamster
    /// </summary>
    public string Species { get; set; } = string.Empty;

    /// <summary>
    /// E.g. Golden retriever, American Staffordshire Terrier
    /// </summary>
    public string Breed { get; set; } = string.Empty;
    
    /// <summary>
    /// Date of arrival at the shelter
    /// </summary>
    public DateOnly ArrivalDate { get; set; }

    public string Age { get; set; } = string.Empty;

    public string HealthStatus { get; set; } = string.Empty;

    public bool VaccinationStatus { get; set; }

    public bool Adopted { get; set; }


    public virtual Shelter? Shelter { get; set; }
    
    public virtual User? AssignedUser { get; set; }
    
    public virtual IList<Adoption> Adoptions { get; set; }
    public virtual IList<Departure> Departures { get; set; }
    public virtual IList<VetVisit> VetVisits { get; set; }
}