using System;
using System.Collections.Generic;

namespace Master.Data.Entities;

public partial class MstCourtType
{
    public int CourtTypeId { get; set; }

    public string? CourtTypeName { get; set; }

    public string? CourtTypeShortName { get; set; }

    public int? OrderNo { get; set; }

    public bool? Active { get; set; }

    public long? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public long? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public long? DeleteBy { get; set; }

    public DateTime? DeleteOn { get; set; }
}
