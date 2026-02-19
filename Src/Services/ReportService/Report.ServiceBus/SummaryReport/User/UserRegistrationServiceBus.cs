using Report.Dto.Global;
using Report.Dto.SummaryReports.User;
using Report.Repository.UnitOfwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Core.Common;

namespace Report.ServiceBus.SummaryReport.User
{
    public class UserRegistrationServiceBus(IUnitOfWorkRepository IUnitOfWorkRepository) : IUserRegistrationServiceBus
    {
        private readonly AsyncLocker<string> userLock = new AsyncLocker<string>();
        private readonly IUnitOfWorkRepository _IUnitOfWorkRepository = IUnitOfWorkRepository;

        public ResponseModel GetUserRegistrationSummaryReport(Nullable<int> departmentId, Nullable<int> unitId, Nullable<int> office, Nullable<int> roleId, int pageSize, int currentPage)
        {
            try
            {
                var data = _IUnitOfWorkRepository.UserRegistration.GetUserRegistrationSummaryReport(departmentId, unitId, office, roleId,pageSize,currentPage);
                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
