using System;
using System.Collections.Generic;

namespace Master.Data.Entities;

public partial class MstSubjectSubMatter
{
    public int SubjectSubMatterId { get; set; }

    public string SubjectSubMatterName { get; set; } = null!;

    public int SubjectMatterId { get; set; }

    public bool? Active { get; set; }

    public long? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public long? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public long? DeleteBy { get; set; }

    public DateTime? DeleteOn { get; set; }
}
