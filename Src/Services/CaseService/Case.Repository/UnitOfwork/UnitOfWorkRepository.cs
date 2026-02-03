using Case.Repository.DierRegistrations;

namespace Case.Repository.UnitOfwork
{
    public class UnitOfWorkRepository(IDierRegistrationsRepository DierRegistrations) : IUnitOfWorkRepository
    {
        public IDierRegistrationsRepository DierRegistrations { get; set; } = DierRegistrations;
    }
}
