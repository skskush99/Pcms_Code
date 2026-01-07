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
    public class APIConfigModel
    {
        [DataMember]
        public Int32 Id { get; set; }

        [DataMember]
        public Int32 OrganizationId { get; set; }

        [DataMember]
        public string ApiKey { get; set; }

        [DataMember]
        public string ModuleName { get; set; }

        [DataMember]
        public string ApiType { get; set; }

        [DataMember]
        public string ClassName { get; set; }

        [DataMember]
        public string FunctionName { get; set; }

        [DataMember]
        public string Description { get; set; }
        public Int32 CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        [DataMember]
        public string FullPath
        {
            get
            {
                return ClassName + "/" + FunctionName;
            }
        }
    }
}
