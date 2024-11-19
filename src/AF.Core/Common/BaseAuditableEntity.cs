namespace AF.Core.Common;

public class BaseAuditableEntity : BaseEntity, IHasId
{
    public DateTimeOffset Created { get; set; }
    public string? CreatedBy { get; set; }
    public DateTimeOffset? LastModified { get; set; }
    public string? LastModifiedBy { get; set; }
}
