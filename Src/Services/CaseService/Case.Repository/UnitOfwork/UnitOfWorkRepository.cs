using Case.Repository.CaseDecision;
using Case.Repository.CaseFileRegister;
using Case.Repository.CaseHearings;
using Case.Repository.CaseRegistrations;
using Case.Repository.CasesDecidedOnIstHearing;
using Case.Repository.DierRegistrations;

namespace Case.Repository.UnitOfwork
{
    public class UnitOfWorkRepository(ICaseDecisionRepository CaseDecision, ICaseFileRegisterRepository CaseFileRegisterRepository, ICaseHearingsRepository CaseHearings, 
        ICaseRegistrationsRepository CaseRegistrations, ICasesDecidedOnIstHearingRepository CasesDecidedOnIstHearing, IDierRegistrationsRepository DierRegistrations) : IUnitOfWorkRepository
    {
        public ICaseDecisionRepository CaseDecision { get; set; } = CaseDecision;
        public ICaseFileRegisterRepository CaseFileRegister { get; set; } = CaseFileRegisterRepository;
        public ICaseHearingsRepository CaseHearings { get; set; } = CaseHearings;
        public ICaseRegistrationsRepository CaseRegistrations { get; set; } = CaseRegistrations;        
        public ICasesDecidedOnIstHearingRepository CasesDecidedOnIstHearing { get; } = CasesDecidedOnIstHearing;
        public IDierRegistrationsRepository DierRegistrations { get; set; } = DierRegistrations;
    }
}
