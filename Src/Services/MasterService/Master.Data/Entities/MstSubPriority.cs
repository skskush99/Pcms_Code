using System;
using System.Collections.Generic;

namespace Master.Data.Entities;

public partial class MstSubPriority
{
    public int SubPriorityId { get; set; }

    public string SubPriorityName { get; set; } = null!;

    public int PriorityId { get; set; }

    public bool? Active { get; set; }

    public long? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public long? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public long? DeleteBy { get; set; }

    public DateTime? DeleteOn { get; set; }
}
