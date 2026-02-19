using CCTNSServiceBus.CCTNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCTNSServiceBus.UnitOfWork
{
    public class UnitOfWorkService(ICCTNSService CCTNSService) : IUnitOfWorkService
    {
        public ICCTNSService CCTNSService { get; set; } = CCTNSService;
    }
}
