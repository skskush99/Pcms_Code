using Report.Dto.Global;
using Report.Dto.SummaryReports.Action;
using Report.Repository.UnitOfwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Core.Common;

namespace Report.ServiceBus.SummaryReport.Action
{
    public class ActionTakenServiceBus(IUnitOfWorkRepository IUnitOfWorkRepository) : IActionTakenServiceBus
    {
        private readonly AsyncLocker<string> userLock = new AsyncLocker<string>();
        private readonly IUnitOfWorkRepository _IUnitOfWorkRepository = IUnitOfWorkRepository;

        public ResponseModel GetActionToBeTakenReport(Nullable<int> deptId, Nullable<int> unitId, Nullable<int> officeId, Nullable<int> districtId, Nullable<int> oicId, Nullable<int> level, Nullable<int> roleid, string main_Party, int pageSize, int currentPage)
        {
            try
            {
                var data = _IUnitOfWorkRepository.ActionTaken.GetActionToBeTakenReport(deptId, unitId, officeId, districtId, oicId, level, roleid, main_Party, pageSize, currentPage);
                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ResponseModel ActionTobeTakenGridNew(Nullable<int> deptId, Nullable<int> unitId, Nullable<int> officeId, Nullable<int> districtId, Nullable<int> oicId, Nullable<int> level, Nullable<int> roleid, string main_Party, int pageSize, int currentPage)
        {
            try
            {
                var data = _IUnitOfWorkRepository.ActionTaken.ActionTobeTakenGridNew(deptId, unitId, officeId, districtId, oicId, level, roleid, main_Party, pageSize, currentPage);
                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
