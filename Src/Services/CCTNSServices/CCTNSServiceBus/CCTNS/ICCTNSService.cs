using CCTNSDto;
using CCTNSDto.CCTNS;
using CCTNSDto.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCTNSServiceBus.CCTNS
{
    public interface ICCTNSService
    {
        Task<ResponseWithoutPaginationModel> GetAuthToken(CCTNSCredentials data);
        Task<ResponseWithoutPaginationModel> GetDistrictDetail(string state_code, string accessToken, CCTNSCredentials data);
    }
}
