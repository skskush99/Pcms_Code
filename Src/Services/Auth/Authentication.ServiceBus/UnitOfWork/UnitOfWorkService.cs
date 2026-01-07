using Authentication.ServiceBus.Esign;
using Authentication.ServiceBus.Users;
using Authentication.ServiceBus.DropDownsService;

namespace Authentication.ServiceBus.UnitOfWork;

public class UnitOfWorkService(IUserLoginServiceBus UserLogins, IEsignServiceBus Esign, IDropDownsServiceBus DropDownsServiceBus) : IUnitOfWorkService
{
    public IUserLoginServiceBus UserLogins { get; set; } = UserLogins;
    public IEsignServiceBus Esign { get; set; } = Esign;
    public IDropDownsServiceBus DropDownsServiceBus { get; set; } = DropDownsServiceBus;
}
