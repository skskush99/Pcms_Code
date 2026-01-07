using System;
using System.Collections.Generic;

namespace Master.Data.Entities;

public partial class UsrRole
{
    public int RoleId { get; set; }

    public string? RoleName { get; set; }

    public string? Desciption { get; set; }

    public long? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public bool? Active { get; set; }
}
