using Master.Dto.Masters;
using Master.Dto.Shared;

namespace Master.ServiceBus.DesignationService
{
    public interface IDesignationServiceBus
    {
        Task<ResponseModel> GetDesignation(DesignationFilterModel objModel);
        Task<ResponseWithoutPaginationModel> GetDesignationDropdownList();
        Task<ResponseModel> AddEditDesignation(DesignationModel objModel, int UserId);
        Task<ResponseModel> ActiveDeactiveDesignation(DesignationActiveDeactiveModel objModel, int UserId);
        
        //Task<ResponseModel> GetDesignationRajmaster(DesignationFilterModel objModel);
        //Task<ResponseWithoutPaginationModel> GetDesignationRajmasterDropdownList();

        /////////// OISc Designation Mapping Start
        //Task<ResponseModel> GetOICSDesigMapping(OICSDesigMappingFilterModel objModel);
        //Task<ResponseWithoutPaginationModel> GetOICSDesigMappingDropdownList(int AdminDeptId, int UnitId);
        //Task<ResponseModel> AddEditOICSDesigMapping(OICSDesigMappingModel objModel, int UserId);
        //Task<ResponseModel> ActiveDeactiveOICSDesigMapping(OICsDesigActiveDeactiveModel objModel, int UserId);
        /////////// OISc Designation Mapping End

        /////////// OISc Designation Section Start
        //Task<ResponseModel> GetSection(SectionFilterModel objModel);
        //Task<ResponseWithoutPaginationModel> GetSectionDropdownList(int AdmDeptId, int UnitId);
        //Task<ResponseModel> AddEditSection(SectionModel objModel, int UserId);
        //Task<ResponseModel> ActiveDeactiveSection(SectionActiveDeactiveModel objModel, int UserId);
        /////////// OISc Designation Section End
    }
}
