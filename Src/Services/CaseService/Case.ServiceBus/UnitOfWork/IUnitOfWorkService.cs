using Case.ServiceBus.DierRegistrationsService;
using Case.ServiceBus.ComplaintRegisterService;
using Case.ServiceBus.DierRegistrations_NewService;

namespace Case.ServiceBus.UnitOfWork
{
    public interface IUnitOfWorkService
    {
        IDierRegistrationsServiceBus DierRegistrationsService { get; }
        IComplaintRegisterServiceBus ComplaintRegisterService { get; }
        IDierRegistrations_NewServiceBus DierRegistrations_NewService { get; }
    }
}
