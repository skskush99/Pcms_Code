using System;
using System.Collections.Generic;

namespace Master.Data.Entities;

public partial class MstAdmDept
{
    public int AdmDeptId { get; set; }

    public int? NicDeptId { get; set; }

    public string? AdmDeptName { get; set; }

    public string? AdmDeptShortName { get; set; }

    public string? MajorMinor { get; set; }

    public bool? Active { get; set; }

    public long? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public long? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public long? DeleteBy { get; set; }

    public DateTime? DeleteOn { get; set; }
}
