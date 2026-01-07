using Sms.ServiceBus.SmsService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sms.ServiceBus.UnitOfWork
{
    public class UnitOfWorkService(ISmsService smsService) : IUnitOfWorkService
    {
        public ISmsService SmsService  { get; set; } = smsService;
    }
}
