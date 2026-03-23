using Case.Repository.DierRegistrations;
using Case.Repository.ComplaintRegister;
using Case.Repository.DierRegistrations_New;

namespace Case.Repository.UnitOfwork
{
    public class UnitOfWorkRepository(IDierRegistrationsRepository DierRegistrations, IComplaintRegisterRepository ComplaintRegister, IDierRegistrations_NewRepository DierRegistrations_New) : IUnitOfWorkRepository
    {
        public IDierRegistrationsRepository DierRegistrations { get; set; } = DierRegistrations;
        public IComplaintRegisterRepository ComplaintRegister { get; set; } = ComplaintRegister;
        public IDierRegistrations_NewRepository DierRegistrations_New { get; set; } = DierRegistrations_New;
    }
}
