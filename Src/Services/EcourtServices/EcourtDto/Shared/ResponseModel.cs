using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcourtDto.Shared
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
        public dynamic? Data { get; set; }
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
}
