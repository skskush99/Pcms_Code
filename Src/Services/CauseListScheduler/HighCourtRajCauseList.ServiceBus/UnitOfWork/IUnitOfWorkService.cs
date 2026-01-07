using HighCourtRajCauseList.ServiceBus.HighCourtRajCauseList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HighCourtRajCauseList.ServiceBus.UnitOfWork
{
    public interface IUnitOfWorkService
    {
        IHighCourtRajCauseListService HighCourtRajCauseListService { get; set; }
    }
}
