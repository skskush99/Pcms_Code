namespace Authentication.ServiceBus.Esign
{
    public interface IEsignServiceBus
    {
        Task<object> AddEsignData(string txn, string esignData);
    }
}
