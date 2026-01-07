namespace Core
{
    public enum Master
    {
    }
    public enum EDbConnectionTypes
    {
        SQL,
        DOCUMENT,
        XML
    }

    public enum AccessRoles
    {
        SA = 1,
        Department = 2,
        Unit = 3,
        Office = 4,
        OIC = 5,
        SAD = 6,
        DepartmentD = 7,
        NodalHod = 8,
        Lawyer = 9,
        StateNodal = 10,
        LawDept = 11,
        JusticeStaff = 14,
    }

    public enum CaseType
    {
        CaseRegistered = 1,
        CaseWithoutCaseNo = 2,
        CasesDecided1stHearing = 3,
    }
}
