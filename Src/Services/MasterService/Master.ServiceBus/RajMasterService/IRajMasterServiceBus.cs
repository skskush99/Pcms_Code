using Master.Dto.Masters;
using Master.Dto.Shared;

namespace Master.ServiceBus.RajMasterService
{
    public interface IRajMasterServiceBus
    {
        Task<ResponseModel> AddStateRajMaster(RajMasterModel objModel, int MasterDataID);
        Task<ResponseModel> AddDivisionRajMaster(RajMasterModel objModel, int MasterDataID);
        Task<ResponseModel> AddDistrictRajMaster(RajMasterModel objModel, int MasterDataID);
        Task<ResponseModel> AddPoliceRangeRajMaster(RajMasterModel objModel, int MasterDataID);
        Task<ResponseModel> AddPoliceDistrictRajMaster(RajMasterModel objModel, int MasterDataID);
        Task<ResponseModel> AddPoliceCircleRajMaster(RajMasterModel objModel, int MasterDataID);
        Task<ResponseModel> AddPoliceStationRajMaster(RajMasterModel objModel, int MasterDataID);



        //Task<ResponseModel> AddCityRajMaster(RajMasterModel objModel, int MasterDataID);
        //Task<ResponseModel> AddSubDivisionRajMaster(RajMasterModel objModel, int MasterDataID);
        //Task<ResponseModel> AddTehsilRajMaster(RajMasterModel objModel, int MasterDataID);
        //Task<ResponseModel> AddDesignationRajMaster(RajMasterModel objModel, int MasterDataID);
        //Task<ResponseModel> AddAdminDepartmentRajMaster(RajMasterModel objModel, int MasterDataID);
        //Task<ResponseModel> AddAdminUnitsDepartmentRajMaster(RajMasterModel objModel, int MasterDataID);


    }
}
