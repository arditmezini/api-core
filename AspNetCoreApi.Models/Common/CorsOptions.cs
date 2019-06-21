using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCoreApi.Models.Common
{
    public class CorsOptions
    {
        public string PolicyName { get; set; }
        public string CorsOrigin { get; set; }
    }
}
