using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using buzzerApi.Enum;
using buzzerApi.Models;
using buzzerApi.Options;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace buzzerApi.Controllers
{
    [Route("api/[controller]"), ApiController]
    public class UploadController : ControllerBase
    {
        // POST api/values
        [HttpPost("Media/{key}"), Authorize]
        public async Task<IActionResult> Media([FromServices] IOptions<UploadOptions> uploadOptions, MediaType key, ICollection<IFormFile> files)
        {
            var options = uploadOptions.Value;
            string directoryPath = null;
            ICollection<string> allowedExtensions = new List<string>();
            if(key == MediaType.Image)
            {
                directoryPath = options.MediaImage;
                allowedExtensions = options.ExtensionsImage.Split(',');
            }
            else if(key == MediaType.Audio)
            {
                directoryPath = options.MediaAudio;
                allowedExtensions = options.ExtensionsAudio.Split(',');
            }

            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);
            try
            {
                ICollection<string> filenames = new List<string>();
                foreach (var file in files)
                {
                    string filePath = null;
                    var fileExt = System.IO.Path.GetExtension(file.FileName).Substring(1);
                    string filename = $"{Guid.NewGuid()}_{file.FileName.Replace(" ", "-")}";
                    if (allowedExtensions.Any(x => x == fileExt))
                    {
                        if (file.Length > 0)
                        {
                            filePath = Path.Combine(directoryPath, filename);
                            filePath = filePath.Replace("\\","/");
                            using (var fileStream = new FileStream(filePath, FileMode.Create))
                            {
                                await file.CopyToAsync(fileStream);
                            }
                            filenames.Add(filePath);
                        }
                        else
                        {
                            return NoContent();
                        }
                    }
                    else
                    {
                        return BadRequest("Unrecognized media type");
                    }
                }
                return Ok(filenames);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}