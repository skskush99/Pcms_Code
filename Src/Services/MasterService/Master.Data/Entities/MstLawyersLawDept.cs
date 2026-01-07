using System;
using System.Collections.Generic;

namespace Master.Data.Entities;

public partial class MstLawyersLawDept
{
    public int Id { get; set; }

    public int LawyerId { get; set; }

    public string? Court { get; set; }

    public int? Rank { get; set; }

    public string? EnrollNo { get; set; }

    public int? Type { get; set; }

    public string? Name { get; set; }

    public string? MobileNo { get; set; }

    public string? EmailId { get; set; }

    public int? LawyerStatus { get; set; }

    public bool? Active { get; set; }

    public long? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public long? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }
}
