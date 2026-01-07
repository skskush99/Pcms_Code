using System;
using System.Collections.Generic;

namespace Master.Data.Entities;

public partial class MstSubjectSubCategory
{
    public int SubjectSubCategoryId { get; set; }

    public string SubjectSubCategoryName { get; set; } = null!;

    public int SubjectCategoryId { get; set; }

    public bool? Active { get; set; }

    public long? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public long? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public long? DeleteBy { get; set; }

    public DateTime? DeleteOn { get; set; }
}
