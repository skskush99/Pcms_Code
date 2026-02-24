using Case.Repository.DierRegistrations;
using Case.Repository.ComplaintRegister;

namespace Case.Repository.UnitOfwork
{
    public class UnitOfWorkRepository(IDierRegistrationsRepository DierRegistrations, IComplaintRegisterRepository ComplaintRegister ) : IUnitOfWorkRepository
    {
        public IDierRegistrationsRepository DierRegistrations { get; set; } = DierRegistrations;
        public IComplaintRegisterRepository ComplaintRegister { get; set; } = ComplaintRegister;
    }
}
