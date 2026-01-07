using Master.Dto.Masters;
using Master.Dto.Shared;

namespace Master.ServiceBus.StateService
{
    public interface IStateServiceBus
    {
        Task<ResponseModel> StateList(StateFilterModel objModel);
        Task<ResponseWithoutPaginationModel> GetStateList();
        Task<ResponseModel> DivisionsList(StateFilterModel objModel);
        Task<ResponseWithoutPaginationModel> GetDivisionsList();
        Task<ResponseModel> DistrictsList(DistrictsFilterModel objModel);
        Task<ResponseWithoutPaginationModel> GetDistrictsList(int DivisionId, int StateId);


        //Task<ResponseModel> CityList(CityFilterModel objModel);
        //Task<ResponseModel> GetCityList(int DistrictId);
        //Task<ResponseModel> TehsilsList(CityFilterModel objModel);
        //Task<ResponseModel> GetTehsilsList(int DistrictId);
        //Task<ResponseModel> SubDivisionsList(SubDivisionsFilterModel objModel);
        //Task<ResponseWithoutPaginationModel> GetSubDivisionsList(int DivisionId, int DistrictId);

    }
}
