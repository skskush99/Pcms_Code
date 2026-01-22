using Case.Repository.CaseDecision;
using Case.Repository.CaseFileRegister;
using Case.Repository.CaseHearings;
using Case.Repository.CaseRegistrations;
using Case.Repository.CasesDecidedOnIstHearing;
using Case.Repository.DierRegistrations;

namespace Case.Repository.UnitOfwork
{
    public interface IUnitOfWorkRepository
    {
        ICaseDecisionRepository CaseDecision { get; }
        ICaseFileRegisterRepository CaseFileRegister { get; }
        ICaseHearingsRepository CaseHearings { get; }
        ICaseRegistrationsRepository CaseRegistrations { get; }        
        ICasesDecidedOnIstHearingRepository CasesDecidedOnIstHearing { get; }
        IDierRegistrationsRepository DierRegistrations { get; }
    }
}
