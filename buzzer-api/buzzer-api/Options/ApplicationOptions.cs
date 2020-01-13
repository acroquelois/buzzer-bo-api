using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace buzzerApi.Options
{
    public class ApplicationOptions
    {
        public AuthOptions Auth { get; set; }

        public UploadOptions Upload { get; set; }

        public LogEventOptions LogEvent { get; set; }
    }
}
