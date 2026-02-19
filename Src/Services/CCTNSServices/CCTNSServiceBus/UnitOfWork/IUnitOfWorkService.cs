using CCTNSServiceBus.CCTNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCTNSServiceBus.UnitOfWork
{
    public interface IUnitOfWorkService
    {
        ICCTNSService CCTNSService { get; }
    }
}
