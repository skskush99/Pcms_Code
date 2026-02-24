using Case.ServiceBus.DierRegistrationsService;
using Case.ServiceBus.ComplaintRegisterService;

namespace Case.ServiceBus.UnitOfWork
{
    public interface IUnitOfWorkService
    {
        IDierRegistrationsServiceBus DierRegistrationsService { get; }
        IComplaintRegisterServiceBus ComplaintRegisterService { get; }
    }
}
