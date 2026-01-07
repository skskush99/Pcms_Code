using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    [Serializable]
    [DataContract]
#nullable disable
    public class ApiRequestModel
    {
        [DataMember]
        public string ApiKey { get; set; }

        [DataMember]
        public Int32 OrgId { get; set; }

        [DataMember]
        public dynamic ApiParams { get; set; }

        [DataMember]
        public Int64 ResponseId { get; set; }
        [DataMember]
        public string Path { get; set; }
        [DataMember]
        public string Module { get; set; }
        [DataMember]
        public Int64 UserId { get; set; }
        [DataMember]
        public dynamic UserDetails { get; set; }
        [DataMember]
        public dynamic UserSessionModel { get; set; }
        [DataMember]
        public string Language { get; set; }
        [DataMember]
        public Int32? FormAction { get; set; }
    }

    [Serializable]
    [DataContract]
#nullable disable
    public class ApiFilterModel
    {
        [DataMember]
        public string SearchText { get; set; }
        [DataMember]
        public int CurrentPageNumber { get; set; }
        [DataMember]
        public int? PageSize { get; set; }
        [DataMember]
        public int? FilterType { get; set; }
        [DataMember]
        public string SortBy { get; set; }
        [DataMember]
        public string SortOrder { get; set; }
        [DataMember]
        public dynamic FilterParams { get; set; }
        [DataMember]
        public string Language { get; set; }
        [DataMember]
        public Int32? ExportFilterType { get; set; }
        [DataMember]
        public Int32? ExportType { get; set; }
        [DataMember]
        public string AppliedFilter { get; set; }
    }
}
