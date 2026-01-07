using Authentication.ServiceBus.Esign;
using Authentication.ServiceBus.Users;
using Authentication.ServiceBus.DropDownsService;


namespace Authentication.ServiceBus.UnitOfWork
{
    public interface IUnitOfWorkService
    {
        IUserLoginServiceBus UserLogins { get; set; }
        IEsignServiceBus Esign { get; set; }
        IDropDownsServiceBus DropDownsServiceBus { get; set; }
    }
}
