using System;
using System.Collections.Generic;

namespace Master.Data.Entities;

public partial class MstCaseAbbrevation
{
    public int AbbrevationId { get; set; }

    public string AbbrevationName { get; set; } = null!;

    public string AbbrevationShort { get; set; } = null!;

    public string? Bench { get; set; }

    public string? Misc { get; set; }

    public int? CatId { get; set; }

    public bool Active { get; set; }

    public long? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public long? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public long? DeletedBy { get; set; }

    public DateTime? DeletedOn { get; set; }
}
