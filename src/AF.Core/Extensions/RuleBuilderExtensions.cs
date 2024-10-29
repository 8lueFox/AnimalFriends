using AF.Core.Database.Repositories;
using AF.Core.Validators;
using FluentValidation;

namespace AF.Core.Extensions;

internal static class RuleBuilderExtensions
{
    public static IRuleBuilderOptions<TRequest, Guid> EntityExists<TRequest, TEntity>(
        this IRuleBuilder<TRequest, Guid> ruleBuilder,
        IRepositoryBase<TEntity> repository)
        where TRequest : class where TEntity : class, IHasId
    {
        return ruleBuilder.SetAsyncValidator(new EntityExistsValidator<TRequest, TEntity>(repository));
    }

    public static IRuleBuilderOptions<TRequest, string> IsUnique<TRequest, TEntity>(this IRuleBuilder<TRequest, string> ruleBuilder,
        IRepositoryBase<TEntity> repository) where TRequest : class where TEntity : class, IHasIdWithName
    {
        return ruleBuilder.SetAsyncValidator(new NameUniquenessValidator<TRequest, TEntity>(repository));
    }
}