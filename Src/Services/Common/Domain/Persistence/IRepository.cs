namespace ShareRecipe.Services.Common.Domain.Persistence
{
    public interface IRepository<T> where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}