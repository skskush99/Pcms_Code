
using Case.Repository.DierRegistrations;
using Case.Repository.ComplaintRegister;

namespace Case.Repository.UnitOfwork
{
    public interface IUnitOfWorkRepository
    {
        IDierRegistrationsRepository DierRegistrations { get; }
        IComplaintRegisterRepository ComplaintRegister { get; }
    }
}
