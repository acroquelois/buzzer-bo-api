using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace buzzerApi.Options
{
    public class UploadOptions
    {
        public string MediaImage { get; set; }

        public string MediaAudio { get; set; }

        public string ExtensionsImage { get; set; }

        public string ExtensionsAudio { get; set; }
    }
}
