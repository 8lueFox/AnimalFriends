namespace AF.Domain.Entities;

public class Departure : BaseAuditableEntity
{
    public Guid AnimaId { get; set; }

    public Guid UserId { get; set; }

    public DateTime DepartureDate { get; set; }

    public string? Notes { get; set; }


    public virtual Animal? Animal { get; set; }

    public virtual User? User { get; set; }
}