namespace AF.Core.Database.Entities;

public class Departure : BaseAuditableEntity
{
    public Guid AnimalId { get; set; }

    public Guid UserId { get; set; }

    public DateTimeOffset DepartureDate { get; set; }

    public string? Notes { get; set; }


    public virtual Animal? Animal { get; set; }

    public virtual User? User { get; set; }
}