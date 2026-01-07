using System;
using System.Collections.Generic;

namespace Master.Data.Entities;

public partial class ReqInformation
{
    public int InfoId { get; set; }

    public string ReqInforamtion { get; set; } = null!;

    public string ShortReqInforamtion { get; set; } = null!;

    public string DesReqInforamtion { get; set; } = null!;

    public int SubjectId { get; set; }

    public int DeptType { get; set; }

    public int DeptId { get; set; }

    public int IsInfoReceived { get; set; }

    public string? Dpdt { get; set; }

    public bool? Active { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public DateTime? SubmitLastDate { get; set; }

    public long? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public long? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public long? DeleteBy { get; set; }

    public DateTime? DeleteOn { get; set; }
}
