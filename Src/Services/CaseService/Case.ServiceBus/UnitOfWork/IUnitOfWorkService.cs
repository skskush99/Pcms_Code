using Case.ServiceBus.DierRegistrationsService;

namespace Case.ServiceBus.UnitOfWork
{
    public interface IUnitOfWorkService
    {
        IDierRegistrationsServiceBus DierRegistrationsService { get; }
    }
}
