using buzzerApi.Enum;
using buzzerApi.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace buzzerApi.Services.Abstraction
{
    public interface IUploadService
    {
        Task<ICollection<String>> UploadMedia(IOptions<UploadOptions> uploadOptions, IOptions<ConnectionOptions> connexionOptions, EnumMediaType key, IFormFileCollection files);
    }
}
