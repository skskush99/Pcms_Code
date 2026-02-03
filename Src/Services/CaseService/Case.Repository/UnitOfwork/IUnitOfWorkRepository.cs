
using Case.Repository.DierRegistrations;

namespace Case.Repository.UnitOfwork
{
    public interface IUnitOfWorkRepository
    {
        IDierRegistrationsRepository DierRegistrations { get; }
    }
}
