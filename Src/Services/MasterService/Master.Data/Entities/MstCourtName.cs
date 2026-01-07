using System;
using System.Collections.Generic;

namespace Master.Data.Entities;

public partial class MstCourtName
{
    public int CourtId { get; set; }

    public int? CourtTypeId { get; set; }

    public int? PlaceId { get; set; }

    public string CourtName { get; set; } = null!;

    public int? StateId { get; set; }

    public bool? Active { get; set; }

    public long? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public long? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public long? DeleteBy { get; set; }

    public DateTime? DeleteOn { get; set; }
}
