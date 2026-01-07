using EcourtDto;
using EcourtDto.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcourtServiceBus.Ecourt
{
    public interface IEcourtService
    {
        Task<ResponseWithoutPaginationModel> GetAuthToken(EcourtCredentials data);
        Task<ResponseWithoutPaginationModel> GetDetailByCNR(string Cinno, string accessToken, EcourtCredentials data);
        Task<ResponseWithoutPaginationModel> GetDetailCaseType(string est_code, string accessToken, EcourtCredentials data);
        Task<ResponseWithoutPaginationModel> GetDetailByCaseNumber(string est_code, string case_type, string reg_no, string reg_year, string accessToken, EcourtCredentials data);
        Task<ResponseWithoutPaginationModel> GetDetailByCNRBulk(string Cinno, string accessToken, EcourtCredentials data);
        Task<ResponseWithoutPaginationModel> GetDetailByPartyName(string est_code, string pend_disp, string litigant_name, string reg_year, string accessToken, EcourtCredentials data);
        Task<ResponseWithoutPaginationModel> GetDistrictDetail(string state_code, string accessToken, EcourtCredentials data);
        Task<ResponseWithoutPaginationModel> GetECourtStateDetail(string accessToken, EcourtCredentials data);
        Task<ResponseWithoutPaginationModel> GetCourtComplexDetail(string state_code, string dist_code, string accessToken, EcourtCredentials data);
        Task<ResponseWithoutPaginationModel> GetCauseListDetail(string est_code, string court_no, string causelist_date, string ci_cri, string accessToken, EcourtCredentials data);
    }
}
