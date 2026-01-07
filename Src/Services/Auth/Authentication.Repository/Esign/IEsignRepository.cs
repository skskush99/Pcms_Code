namespace Authentication.Repository.Esign
{
    public interface IEsignRepository
    {
        Task<object> AddEsignData(string txn, string esignData);
    }
}
