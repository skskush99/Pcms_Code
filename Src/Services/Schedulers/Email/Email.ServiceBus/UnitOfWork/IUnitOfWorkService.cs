using Email.ServiceBus.EmailService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Email.ServiceBus.UnitOfWork
{
    public interface IUnitOfWorkService
    {
        IEmailServices EmailServices { get; set; }
    }
}
