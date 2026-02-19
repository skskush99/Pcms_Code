using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report.Dto.Global
{
    public class ApplicationTransactionMessage
    {
        public enum MessageState
        {
            Success,
            Warning,
            Error,
            Information
        }
        public MessageState Status { get; set; }
        public string Message { get; set; }
    }
}
