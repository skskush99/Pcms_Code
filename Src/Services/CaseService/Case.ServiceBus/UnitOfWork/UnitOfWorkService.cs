using Case.ServiceBus.DierRegistrationsService;
using Case.ServiceBus.ComplaintRegisterService;

namespace Case.ServiceBus.UnitOfWork
{
    public class UnitOfWorkService(IDierRegistrationsServiceBus DierRegistrationsService, IComplaintRegisterServiceBus ComplaintRegisterService ) : IUnitOfWorkService
    {
        public IDierRegistrationsServiceBus DierRegistrationsService { get; set; } = DierRegistrationsService;
        public IComplaintRegisterServiceBus ComplaintRegisterService { get; set; } = ComplaintRegisterService;

    }
}
