
using Case.Repository.DierRegistrations;
using Case.Repository.ComplaintRegister;
using Case.Repository.DierRegistrations_New;

namespace Case.Repository.UnitOfwork
{
    public interface IUnitOfWorkRepository
    {
        IDierRegistrationsRepository DierRegistrations { get; }
        IComplaintRegisterRepository ComplaintRegister { get; }
        IDierRegistrations_NewRepository DierRegistrations_New { get; }
    }
}
