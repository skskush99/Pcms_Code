using Case.Dto.ComplaintRegister;
using Case.Dto.DierRegistrations;
using Case.Dto.Shared;


namespace Case.Repository.ComplaintRegister
{
    public interface IComplaintRegisterRepository
    {
        Task<ResponseModel> GetComplaintList(ComplaintListFilterModel objModel);
        Task<ComplaintRegisterResponseModel> AddEditComplaintRegister(ComplaintRegisterModel objModel, int UserId);

        Task<ResponseWithoutPaginationModel> GetPersonAgainstDetails(long ComplaintRegId);
        Task<ComplaintRegisterResponseModel> AddEditPersonAgainstDetails(PersonAgainstDetailsModel objModel, int UserId);
        Task<ComplaintRegisterResponseModel> DeletePersonAgainstDetails(int PersonAgainstId, int UserId);
    }


}
