using AF.Core.Database.Repositories;
using AF.Core.Exceptions;
using FluentValidation;
using FluentValidation.Validators;

namespace AF.Core.Validators;

public class EntityExistsValidator<TRequest, TEntity>(IRepositoryBase<TEntity> repository)
    : AsyncPropertyValidator<TRequest, Guid>
    where TEntity : class, IHasId
{
    public override async Task<bool> IsValidAsync(ValidationContext<TRequest> context, Guid value, CancellationToken cancellation)
    {
        if (!await QueryableHelper.Any(repository.Items, x => x.Id == value, cancellation))
            throw new EntityDoesNotExistException($"{typeof(TEntity).Name} {value} does not exists");

        return true;
    }

    public override string Name => "EntityExistsValidator";
}