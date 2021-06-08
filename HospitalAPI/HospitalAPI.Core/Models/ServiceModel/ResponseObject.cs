using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalAPI.Core.Models.ServiceModel
{
    public class ResponseObject
    {
        public bool IsValid { get; set; }
        public string Message { get; set; }
        public dynamic Data { get; set; }
    }
}
