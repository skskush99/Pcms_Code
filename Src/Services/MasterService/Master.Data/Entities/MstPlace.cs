using System;
using System.Collections.Generic;

namespace Master.Data.Entities;

public partial class MstPlace
{
    public int PlaceId { get; set; }

    public string? PlaceName { get; set; }

    public int? TehsilId { get; set; }

    public int? DistrictId { get; set; }

    public bool? Active { get; set; }

    public long? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public long? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public long? DeleteBy { get; set; }

    public DateTime? DeleteOn { get; set; }
}
