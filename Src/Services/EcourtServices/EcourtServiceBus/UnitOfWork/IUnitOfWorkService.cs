using EcourtServiceBus.Ecourt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcourtServiceBus.UnitOfWork
{
    public interface IUnitOfWorkService
    {
        IEcourtService EcourtService { get; }
    }
}
