using Authentication.Repository.Esign;
using Authentication.Repository.UserToken;
using Authentication.Repository.DropDowns;

namespace Authentication.Repository.UnitOfwork;

public class UnitOfWorkRepository(IUserLogin UserLogins, IEsignRepository Esign, IDropDowns DropDowns) : IUnitOfWorkRepository
{
    public IUserLogin UserLogins { get; set; } = UserLogins;
    public IEsignRepository Esign { get; set; } = Esign;
    public IDropDowns DropDowns { get; set; } = DropDowns;



}
