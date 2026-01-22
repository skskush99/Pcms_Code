using Case.ServiceBus.CaseDecision;
using Case.ServiceBus.CaseFileRegister;
using Case.ServiceBus.CaseHearings;
using Case.ServiceBus.CaseRegistrations;
using Case.ServiceBus.CasesDecidedOnIstHearing;
using Case.ServiceBus.DierRegistrationsService;

namespace Case.ServiceBus.UnitOfWork
{
    public interface IUnitOfWorkService
    {
        ICaseDecisionServiceBus CaseDecision { get; }
        ICaseFileRegisterServiceBus CaseFileRegisterServiceBus { get; }
        ICaseHearingsServiceBus CaseHearings { get; }
        ICaseRegistrationsServiceBus CaseRegistrations { get; }
        ICasesDecidedOnIstHearingServiceBus CasesDecidedOnIstHearing { get; }
        IDierRegistrationsServiceBus DierRegistrationsService { get; }
    }
}
