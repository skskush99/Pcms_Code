using System;
using System.Collections.Generic;

namespace Master.Data.Entities;

public partial class MstOic
{
    public int Oicid { get; set; }

    public int? OfficeId { get; set; }

    public int? UnitId { get; set; }

    public string Name { get; set; } = null!;

    public string? Designation { get; set; }

    public string? Address1 { get; set; }

    public string? Address2 { get; set; }

    public string? Address3 { get; set; }

    public string? Fax { get; set; }

    public string? MobileNo { get; set; }

    public string? ContactNo { get; set; }

    public string? Email { get; set; }

    public string? AdharNo { get; set; }

    public string? EmployeeCode { get; set; }

    public int? TehsilId { get; set; }

    public int? DistrictId { get; set; }

    public bool? Active { get; set; }

    public bool IsRetired { get; set; }

    public string? CancelFlag { get; set; }

    public long? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public long? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public long? DeleteBy { get; set; }

    public DateTime? DeleteOn { get; set; }
}
