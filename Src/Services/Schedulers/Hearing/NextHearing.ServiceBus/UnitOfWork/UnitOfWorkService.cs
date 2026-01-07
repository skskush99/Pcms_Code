using NextHearing.ServiceBus.NextHearingService;

namespace NextHearing.ServiceBus.UnitOfWork
{
    public class UnitOfWorkService(INextHearingService nextHearingService) : IUnitOfWorkService
    {
        public INextHearingService NextHearingService { get; set; } = nextHearingService;
    }
}
