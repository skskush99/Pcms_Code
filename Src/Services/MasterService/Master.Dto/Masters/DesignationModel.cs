namespace Master.Dto.Masters
{
    public class DesignationFilterModel
    {
        public int PageNo { get; set; }
        public int PageSize { get; set; }
        public string? SortBy { get; set; }
        public bool? IsSortByDesc { get; set; }
    }
    public class DesignationModel
    {
        public int DesignationId { get; set; }
        public string DesignationEng { get; set; }
        public string? DesignationHindi { get; set; }
        public int LevelId { get; set; }
        public bool IsActive { get; set; }
        public long CreatedBy { get; set; }
        public long UpdatedBy { get; set; }

    }

    public class DesignationRequestModel
    {
        public required DesignationModel Data { get; set; }
    }

    public class DesignationActiveDeactiveModel
    {
        public int DesignationId { get; set; }
        public bool IsActive { get; set; }
        public long UpdatedBy { get; set; }
    }

    /////////// OISc Designation Mapping Start
    //public class OICSDesigMappingFilterModel
    //{
    //    public int AdminDeptId { get; set; }
    //    public int UnitId { get; set; }
    //    public int? ActiveFilter { get; set; }
    //    public int PageNo { get; set; }
    //    public int PageSize { get; set; }
    //    public string? SortBy { get; set; }
    //    public bool? IsSortByDesc { get; set; }
    //}
    //public class OICSDesigMappingModel
    //{
    //    public int DesignationId { get; set; }
    //    public int AdminDeptId { get; set; }
    //    public int UnitId { get; set; }
    //    public int ExistMstDesignationId { get; set; }
    //    public string? SectionName { get; set; }
    //    public int RajMasterDesignationId { get; set; }
    //    public string RajMasterDesignationName { get; set; }
    //    public long CreatedBy { get; set; }
    //    public long UpdatedBy { get; set; }
    //    public bool? IfBracket { get; set; }

    //}

    //public class OICsDesigActiveDeactiveModel
    //{
    //    public int DesignationId { get; set; }
    //    public bool Active { get; set; }
    //    public long UpdatedBy { get; set; }
    //}

    /////////// OISc Designation Mapping End

    /////////// OISc Designation Section Start
    //public class SectionFilterModel
    //{
    //    public int AdmDeptId { get; set; }
    //    public int UnitId { get; set; }
    //    public int PageNo { get; set; }
    //    public int PageSize { get; set; }
    //    public string? SortBy { get; set; }
    //    public bool? IsSortByDesc { get; set; }
    //}
    //public class SectionModel
    //{
    //    public int SectionId { get; set; }
    //    public string SectionName { get; set; }
    //    public bool Active { get; set; }
    //    public long CreatedBy { get; set; }
    //    public long UpdatedBy { get; set; }
    //    public long DeleteBy { get; set; }

    //}

    //public class SectionActiveDeactiveModel
    //{
    //    public int SectionId { get; set; }
    //    public bool Active { get; set; }
    //    public long UpdatedBy { get; set; }
    //    public long DeleteBy { get; set; }
    //}

    /////////// OISc Designation Section End
}
