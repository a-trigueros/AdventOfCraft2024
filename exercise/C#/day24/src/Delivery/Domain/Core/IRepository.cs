namespace Delivery.Domain.Core
{
    public interface IRepository<TAggregate>
        where TAggregate : EventSourcedAggregate
    {
        TAggregate? FindById(Guid id);
        void Save(TAggregate aggregate);
    }
}