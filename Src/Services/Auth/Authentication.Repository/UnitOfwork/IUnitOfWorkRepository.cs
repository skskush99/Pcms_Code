using Authentication.Repository.Esign;
using Authentication.Repository.UserToken;
using Authentication.Repository.DropDowns;

namespace Authentication.Repository.UnitOfwork
{
    public interface IUnitOfWorkRepository
    {
        IUserLogin UserLogins { get; set; }
        IEsignRepository Esign { get; set; }
        IDropDowns DropDowns { get; set; }
    }
}
