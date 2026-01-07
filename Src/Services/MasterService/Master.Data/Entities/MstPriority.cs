using System;
using System.Collections.Generic;

namespace Master.Data.Entities;

public partial class MstPriority
{
    public int PriorityId { get; set; }

    public string? PriorityName { get; set; }

    public bool? Active { get; set; }
}
