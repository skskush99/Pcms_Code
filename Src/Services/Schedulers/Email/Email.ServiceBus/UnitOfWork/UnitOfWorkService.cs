using Email.ServiceBus.EmailService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Email.ServiceBus.UnitOfWork
{
    public class UnitOfWorkService(IEmailServices emailServices) : IUnitOfWorkService
    {
        public IEmailServices EmailServices { get; set; } = emailServices;
    }
}
