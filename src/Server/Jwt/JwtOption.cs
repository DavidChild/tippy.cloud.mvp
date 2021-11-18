using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tippy.cloud.Server
{
    public class JwtOption
    {
        public string Key {get;set; }
        public string Issuer { get; set; }

        public string Audience { get; set; }
        public double ExpiryInHours { get; set; }
    }
}
