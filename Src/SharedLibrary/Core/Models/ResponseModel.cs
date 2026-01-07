using Core.Enums.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class ResponseModel
    {
        public ResponseModel()
        {
            Status = Status.Success;
        }
        public ResponseModel(Status defaultStatus = Status.Alert)
        {
            Status = defaultStatus;
        }
        private string _msg = "";
        [DataMember]
        public String Message
        {
            get
            {
                if ((int)ReturnMessage != 0)
                {
                    return Utils.EnumUtility.GetDescription(ReturnMessage);
                }
                else
                    return _msg;
            }
            set { _msg = value; }
        }
        public ReturnMessage ReturnMessage { get; set; }
        [DataMember]
        public Status Status { get; set; }
        public string SaveOption { get; set; }
        [DataMember]
        public object EntityId { get; set; }
        [DataMember]
        public dynamic CustomObject { get; set; }
        [DataMember]
        public Int32 TotalRecordCount { get; set; }
        [DataMember]
        public Int32 FilteredRecordCount { get; set; }
    }
}
