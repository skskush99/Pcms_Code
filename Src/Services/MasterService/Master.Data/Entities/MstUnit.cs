using System;
using System.Collections.Generic;

namespace Master.Data.Entities;

public partial class MstUnit
{
    public int UnitId { get; set; }

    public string? UnitName { get; set; }

    public string? UnitShortName { get; set; }

    public int AdmDeptId { get; set; }

    public int? NicUnitId { get; set; }

    public bool? Active { get; set; }

    public long? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public long? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public long? DeleteBy { get; set; }

    public DateTime? DeleteOn { get; set; }
}
