namespace Case.Dto.Shared
{
    public class ResponseModel
    {
        public bool Status { get; set; }
        public string? Message { get; set; }
        public IEnumerable<object>? Data { get; set; }
        public IEnumerable<PaginationModel>? Pagination { get; set; }
    }

    public class ResponseWithoutPaginationModel
    {
        public bool Status { get; set; }
        public string? Message { get; set; }
        public IEnumerable<object>? Data { get; set; }
    }

    public class PaginationModel
    {
        public string? SortBy { get; set; }
        public bool? IsSortByDesc { get; set; }
        public int PageNo { get; set; }
        public int PageSize { get; set; }
        public long TotalRecords { get; set; }
    }

    public class RequestModel
    {
        public required string Tocken { get; set; }
        public required object Data { get; set; }
    }

    public class TokenAuthModel
    {
        public string? Token { get; set; }
        public bool Status { get; set; }
        public string? Message { get; set; }
        public long UserId { get; set; }
        public long RoleId { get; set; }
        public string? LoginOn { get; set; }
        public string? IPAddress { get; set; }
    }

    public class ActiveDeactiveModel
    {
        public required string Tocken { get; set; }
        public int Id { get; set; }
        public bool Status { get; set; }
        public long ActionBy { get; set; }
    }

    public class DropdownlistModel
    {
        public required string Text { get; set; }
        public required string Value { get; set; }
    }
}
