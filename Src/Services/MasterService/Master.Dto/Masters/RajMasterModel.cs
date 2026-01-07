namespace Master.Dto.Masters
{
    public class RajMasterModel
    {
        public int MasterDataID { get; set; }

    }
    public class AddDistrictOutSideRajModel
    {
        public int? DistrictId { get; set; }
        //public int? DistrictMaster { get; set; }
        public int StateId { get; set; }
        public int DivisionId { get; set; }
        public string DistrictNameEng { get; set; }
        public bool Active { get; set; }
        public string CreatedBy { get; set; }
        public long LastUpdatedBy { get; set; }

    }
    public class DeactiveDistrictOutSideRajModel
    {
        public int DistrictId { get; set; }
        public bool Active { get; set; }
        public long LastUpdatedBy { get; set; }

    }
    public class AddTehsilOutSideRajModel
    {
        public int? TehsilId { get; set; }
        //public int TehsilMasterId { get; set; }
        public int DistrictId { get; set; }
        public string TehsilNameEng { get; set; }
        public bool Active { get; set; }
        public string CreatedBy { get; set; }
        public long LastUpdatedBy { get; set; }

    }
    public class DeactiveTehsilOutSideRajModel
    {
        public int TehsilId { get; set; }
        public bool Active { get; set; }
        public long LastUpdatedBy { get; set; }

    }
    public class AddCityOutSideRajModel
    {
        public int? CityId { get; set; }
        public int DistrictId { get; set; }
        public string CityNameEng { get; set; }
        public bool Active { get; set; }
        public string CreatedBy { get; set; }
        public long LastUpdatedBy { get; set; }

    }
    public class DeactiveCityOutSideRajModel
    {
        public int CityId { get; set; }
        public bool Active { get; set; }
        public long LastUpdatedBy { get; set; }

    }

}
