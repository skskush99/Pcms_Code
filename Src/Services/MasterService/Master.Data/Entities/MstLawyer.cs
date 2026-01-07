using System;
using System.Collections.Generic;

namespace Master.Data.Entities;

public partial class MstLawyer
{
    public int LawyerId { get; set; }

    public string? Name { get; set; }

    public string? ShortName { get; set; }

    public string? Address1 { get; set; }

    public string? Address2 { get; set; }

    public string? Address3 { get; set; }

    public string? ContactNo { get; set; }

    public string? MobileNo { get; set; }

    public string? EmailId { get; set; }

    public string? Fax { get; set; }

    public bool? IsGla { get; set; }

    public DateTime? GlafromDate { get; set; }

    public DateTime? GlatoDate { get; set; }

    public int? DistrictId { get; set; }

    public int? TehsilId { get; set; }

    public int? Ldesignation { get; set; }

    public string? UserName { get; set; }

    public string? UserPassword { get; set; }

    public string? ConfirmPassword { get; set; }

    public int? RoleId { get; set; }

    public string? EnrollNo { get; set; }

    public string? Salutation { get; set; }

    public bool? Active { get; set; }

    public bool? IsInactive { get; set; }

    public long? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public long? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public long? DeleteBy { get; set; }

    public DateTime? DeleteOn { get; set; }
}
