using System;
using System.Collections.Generic;

namespace Master.Data.Entities;

public partial class MstHeadName
{
    public int HeadId { get; set; }

    public string? HeadName { get; set; }

    public int? Status { get; set; }

    public bool? Active { get; set; }

    public long? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public long? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public long? DeleteBy { get; set; }

    public DateTime? DeleteOn { get; set; }
}
