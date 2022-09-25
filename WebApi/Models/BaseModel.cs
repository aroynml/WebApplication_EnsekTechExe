using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class BaseModel
    {
        public class meterreadingupload_request
        {
        }

        public class meterreadingupload_result
        {
            public int success_readings { get; set; } = 0;
            public int failed_readings { get; set; } = 0;

        }
    }
}
