using NextHearing.ServiceBus.NextHearingService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextHearing.ServiceBus.UnitOfWork
{
    public interface IUnitOfWorkService
    {
        INextHearingService NextHearingService { get; set; }
    }
}
