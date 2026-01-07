using Microsoft.EntityFrameworkCore;

namespace Master.Data.Entities;

public partial class MasterDbContext : DbContext
{
    public MasterDbContext()
    {
    }

    public MasterDbContext(DbContextOptions<MasterDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<MstAdmDept> MstAdmDepts { get; set; }

    public virtual DbSet<MstCaseAbbrevation> MstCaseAbbrevations { get; set; }

    public virtual DbSet<MstCourtName> MstCourtNames { get; set; }

    public virtual DbSet<MstCourtType> MstCourtTypes { get; set; }

    public virtual DbSet<MstDesignation> MstDesignations { get; set; }

    public virtual DbSet<MstHeadName> MstHeadNames { get; set; }

    public virtual DbSet<MstLawyer> MstLawyers { get; set; }

    public virtual DbSet<MstLawyersLawDept> MstLawyersLawDepts { get; set; }

    public virtual DbSet<MstOffice> MstOffices { get; set; }

    public virtual DbSet<MstOic> MstOics { get; set; }

    public virtual DbSet<MstPlace> MstPlaces { get; set; }

    public virtual DbSet<MstPriority> MstPriorities { get; set; }

    public virtual DbSet<MstSubPriority> MstSubPriorities { get; set; }

    public virtual DbSet<MstSubjectCategory> MstSubjectCategories { get; set; }

    public virtual DbSet<MstSubjectMatter> MstSubjectMatters { get; set; }

    public virtual DbSet<MstSubjectSubCategory> MstSubjectSubCategories { get; set; }

    public virtual DbSet<MstSubjectSubMatter> MstSubjectSubMatters { get; set; }

    public virtual DbSet<MstUnit> MstUnits { get; set; }

    public virtual DbSet<ReqInformation> ReqInformations { get; set; }

    public virtual DbSet<TrnGroupingMaster> TrnGroupingMasters { get; set; }

    public virtual DbSet<UsrRole> UsrRoles { get; set; }

    public virtual DbSet<UsrUserLogin> UsrUserLogins { get; set; }

}
