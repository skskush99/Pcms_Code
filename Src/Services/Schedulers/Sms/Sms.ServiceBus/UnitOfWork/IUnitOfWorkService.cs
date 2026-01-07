using Sms.ServiceBus.SmsService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sms.ServiceBus.UnitOfWork
{
    public interface IUnitOfWorkService
    {
        ISmsService SmsService { get; set; }
    }
}
