using System;
using System.Collections.Generic;

namespace Master.Data.Entities;

public partial class MstOffice
{
    public int OfficeId { get; set; }

    public int? UnitId { get; set; }

    public string OfficeName { get; set; } = null!;

    public string? ContactNo { get; set; }

    public string? Fax { get; set; }

    public string? Address { get; set; }

    public int? DivisionId { get; set; }

    public int? DistrictId { get; set; }

    public int? TehsilId { get; set; }

    public bool? Active { get; set; }

    public long? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public long? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public long? DeleteBy { get; set; }

    public DateTime? DeleteOn { get; set; }
}
