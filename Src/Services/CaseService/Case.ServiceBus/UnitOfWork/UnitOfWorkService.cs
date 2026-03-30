using Case.ServiceBus.DierRegistrationsService;
using Case.ServiceBus.ComplaintRegisterService;
using Case.ServiceBus.DierRegistrations_NewService;

namespace Case.ServiceBus.UnitOfWork
{
    public class UnitOfWorkService(IDierRegistrationsServiceBus DierRegistrationsService, IComplaintRegisterServiceBus ComplaintRegisterService, IDierRegistrations_NewServiceBus DierRegistrations_NewService) : IUnitOfWorkService
    {
        public IDierRegistrationsServiceBus DierRegistrationsService { get; set; } = DierRegistrationsService;
        public IComplaintRegisterServiceBus ComplaintRegisterService { get; set; } = ComplaintRegisterService;
        public IDierRegistrations_NewServiceBus DierRegistrations_NewService { get; set; } = DierRegistrations_NewService;

    }
}
