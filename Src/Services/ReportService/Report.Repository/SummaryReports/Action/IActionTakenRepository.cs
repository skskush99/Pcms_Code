using Report.Dto.Global;
using Report.Dto.SummaryReports.Action;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report.Repository.SummaryReports.Action
{
    public interface IActionTakenRepository
    {
        ResponseModel GetActionToBeTakenReport(Nullable<int> deptId, Nullable<int> unitId, Nullable<int> officeId, Nullable<int> districtId, Nullable<int> oicId, Nullable<int> level, Nullable<int> roleid, string main_Party, int pageSize, int currentPage);
        ResponseModel ActionTobeTakenGridNew(Nullable<int> deptId, Nullable<int> unitId, Nullable<int> officeId, Nullable<int> districtId, Nullable<int> oicId, Nullable<int> level, Nullable<int> roleid, string main_Party, int pageSize, int currentPage);
    }
}
