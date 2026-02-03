using Case.ServiceBus.DierRegistrationsService;

namespace Case.ServiceBus.UnitOfWork
{
    public class UnitOfWorkService(IDierRegistrationsServiceBus DierRegistrationsService) : IUnitOfWorkService
    {
        public IDierRegistrationsServiceBus DierRegistrationsService { get; set; } = DierRegistrationsService;
    }
}
