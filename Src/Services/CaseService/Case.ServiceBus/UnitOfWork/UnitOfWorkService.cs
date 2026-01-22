using Case.ServiceBus.CaseDecision;
using Case.ServiceBus.CaseFileRegister;
using Case.ServiceBus.CaseHearings;
using Case.ServiceBus.CaseRegistrations;
using Case.ServiceBus.CasesDecidedOnIstHearing;
using Case.ServiceBus.DierRegistrationsService;

namespace Case.ServiceBus.UnitOfWork
{
    public class UnitOfWorkService(ICaseDecisionServiceBus CaseDecision, ICaseFileRegisterServiceBus CaseFileRegisterServiceBus,
        ICaseHearingsServiceBus CaseHearings, ICaseRegistrationsServiceBus CaseRegistrations, ICasesDecidedOnIstHearingServiceBus CasesDecidedOnIstHearing,
        IDierRegistrationsServiceBus DierRegistrationsService) : IUnitOfWorkService
    {
        public ICaseDecisionServiceBus CaseDecision { get; set; } = CaseDecision;
        public ICaseFileRegisterServiceBus CaseFileRegisterServiceBus { get; set; } = CaseFileRegisterServiceBus;
        public ICaseHearingsServiceBus CaseHearings { get; set; } = CaseHearings;
        public ICaseRegistrationsServiceBus CaseRegistrations { get; set; } = CaseRegistrations;        
        public ICasesDecidedOnIstHearingServiceBus CasesDecidedOnIstHearing { get; set; } = CasesDecidedOnIstHearing;
        public IDierRegistrationsServiceBus DierRegistrationsService { get; set; } = DierRegistrationsService;
    }
}
