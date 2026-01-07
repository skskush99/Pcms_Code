namespace Master.Dto.WebSite
{
    public class WebSitesFIlterModel
    {
        public int CategoryId { get; set; }
        public string? SortBy { get; set; }
        public bool? IsSortByDesc { get; set; }
        public int PageNo { get; set; }
        public int PageSize { get; set; }
    }
    public class WebSitesModel
    {
        public int? Id { get; set; }
        public int? CategoryId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? ImagePath { get; set; }
        public string? LinkURL { get; set; }
        public int? DisplayOrder { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
    }
    public class WebSitesContactAddModel
    {
        public int? Id { get; set; }
        public int? CategoryId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int? DisplayOrder { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
    }


}
