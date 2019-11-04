using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace buzzerApi.Options
{
    public class AuthOptions
    {
        public int ExpireMinutes { get; set; } = 10080; // 7days
        public string SecretKey { get; set; } = "Tw9dsdregfddshfusd";
        public string SecurityAlgorithm { get; set; } = SecurityAlgorithms.HmacSha256Signature;
    }
}
