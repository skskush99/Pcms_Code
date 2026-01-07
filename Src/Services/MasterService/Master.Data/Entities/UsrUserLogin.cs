using System;
using System.Collections.Generic;

namespace Master.Data.Entities;

public partial class UsrUserLogin
{
    public long UserId { get; set; }

    public long? RoleId { get; set; }

    public long? DepartmentId { get; set; }

    public long? UnitId { get; set; }

    public long? OfficeId { get; set; }

    public long? Oicid { get; set; }

    public long? DistrictId { get; set; }

    public long? LawyerId { get; set; }

    public string? Ssoid { get; set; }

    public string? UserName { get; set; }

    public string? Password { get; set; }

    public string? Name { get; set; }

    public DateTime? Dob { get; set; }

    public string? Gender { get; set; }

    public string? Designation { get; set; }

    public string? Mobile { get; set; }

    public string? Contact { get; set; }

    public string? OfficialMail { get; set; }

    public string? PersonalMail { get; set; }

    public string? PostalAddress { get; set; }

    public string? PostalCode { get; set; }

    public string? City { get; set; }

    public string? State { get; set; }

    public string? Photo { get; set; }

    public byte[]? Image { get; set; }

    public string? AadhaarId { get; set; }

    public string? BhamashahId { get; set; }

    public string? BhamashahMemberId { get; set; }

    public string? Ipphone { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public bool? Active { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public bool? Deleted { get; set; }

    public int? DeletedBy { get; set; }

    public DateTime? DeletedOn { get; set; }
}
