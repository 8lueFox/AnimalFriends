namespace AF.Domain.Entities;

public class VetVisit : BaseAuditableEntity
{
    public Guid AnimalId { get; set; }

    public string VetName { get; set; } = string.Empty;

    public DateTime VisitDate { get; set; }

    public string Diagnosis { get; set; } = string.Empty;

    public string Treatment { get; set; } = string.Empty;
    
    
    public virtual Animal? Animal { get; set; }
}