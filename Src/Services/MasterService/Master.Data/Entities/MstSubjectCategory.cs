using System;
using System.Collections.Generic;

namespace Master.Data.Entities;

public partial class MstSubjectCategory
{
    public int SubjectCategoryId { get; set; }

    public string SubjectCategoryName { get; set; } = null!;

    public bool? Active { get; set; }

    public long? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public long? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public long? DeleteBy { get; set; }

    public DateTime? DeleteOn { get; set; }
}
