namespace ImageManagement.Domain.SeedWork
{
    public interface IRepository
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
