using HighCourtRajCauseList.ServiceBus.HighCourtRajCauseList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HighCourtRajCauseList.ServiceBus.UnitOfWork
{
    public class UnitOfWorkService(IHighCourtRajCauseListService HighCourtRajCauseListService) : IUnitOfWorkService
    {
        public IHighCourtRajCauseListService HighCourtRajCauseListService { get; set; } = HighCourtRajCauseListService;
    }
}
