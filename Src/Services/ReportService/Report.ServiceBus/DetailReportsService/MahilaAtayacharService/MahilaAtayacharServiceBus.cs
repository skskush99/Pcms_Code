using Report.Dto.DetailReports;
using Report.Repository.UnitOfwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Core.Common;

namespace Report.ServiceBus.DetailReportsService.MahilaAtayacharService
{
    public class MahilaAtayacharServiceBus(IUnitOfWorkRepository IUnitOfWorkRepository) : IMahilaAtayacharServiceBus
    {
        private readonly AsyncLocker<string> userLock = new AsyncLocker<string>();
        private readonly IUnitOfWorkRepository _IUnitOfWorkRepository = IUnitOfWorkRepository;

        public async Task<DetailReportsResponseModel> GetMahilaAtayacharIPCReport(MahilaAtayacharIPCModel objModel)
        {
            try
            {
                var data = _IUnitOfWorkRepository.MahilaAtayacharReport.GetMahilaAtayacharIPCReport(objModel);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<DetailReportsResponseModel> GetMahilaAtayacharBNSReport(MahilaAtayacharBNSModel objModel)
        {
            try
            {
                var data = _IUnitOfWorkRepository.MahilaAtayacharReport.GetMahilaAtayacharBNSReport(objModel);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
