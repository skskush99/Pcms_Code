using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Email.Dto.Email
{
    public class EmailResponse
    {
    }
    public class ResponseModel
    {
        public bool Status { get; set; }
        public string? Message { get; set; }
        public IEnumerable<object>? Data { get; set; }
    }
}
