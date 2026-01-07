using Master.Dto.Masters;
using Master.Dto.Shared;

namespace Master.ServiceBus.PoliceThanaService
{
    public interface IPoliceThanaServiceBus
    {
        Task<ResponseModel> GetPoliceRange(PoliceRangeFilterModel objModel);
        Task<ResponseWithoutPaginationModel> PoliceRangeDropdownList(int DistrictId);
        Task<ResponseModel> GetPoliceDistrict(PoliceDistrictFilterModel objModel);
        Task<ResponseWithoutPaginationModel> PoliceDistrictDropdownList(int DistrictId, int RangeId);
        Task<ResponseModel> GetPoliceCircle(PoliceCircleFilterModel objModel);
        Task<ResponseWithoutPaginationModel> PoliceCircleDropdownList(int PdId);
        Task<ResponseModel> GetPoliceStation(PoliceStationFilterModel objModel);
        Task<ResponseWithoutPaginationModel> PoliceStationDropdownList(int PcId);

    }
}
