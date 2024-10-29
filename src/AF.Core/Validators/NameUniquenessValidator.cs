using AF.Core.Database.Repositories;
using FluentValidation;
using FluentValidation.Validators;

namespace AF.Core.Validators;

public class NameUniquenessValidator<TRequest, TEntity> : AsyncPropertyValidator<TRequest, string>
where TEntity: class, IHasIdWithName
{
    private static readonly Func<TRequest, Guid>? IdSelector;

    private readonly IRepositoryBase<TEntity> _repository;
    
    static NameUniquenessValidator()
    {
        var idProperty = typeof(TRequest).GetProperty("Id");
        if (idProperty != null)
        {
            IdSelector = (Func<TRequest, Guid>)idProperty.GetMethod.CreateDelegate(typeof(Func<TRequest, Guid>));
        }
        else
        {
            IdSelector = null;
        }
    }
    
    public NameUniquenessValidator(IRepositoryBase<TEntity> repository)
    {
        _repository = repository;
    }
    
    public override async Task<bool> IsValidAsync(ValidationContext<TRequest> context, string? value, CancellationToken cancellation)
    {
        if (value == null) return true;
        if (IdSelector != null)
        {
            var id = IdSelector(context.InstanceToValidate);
            if (!await QueryableHelper.All(_repository.Items, x => x.Name != value || x.Id == id, cancellation))
            {
                return false;
            }
        }
        else
        {
            if (await QueryableHelper.Any(_repository.Items, x => x.Name == value, cancellation))
            {
                return false;
            }
        }

        return true;
    }

    public override string Name => "NameUniquenessValidator";
}