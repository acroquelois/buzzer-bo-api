using buzzerApi.Enum;
using buzzerApi.Options;
using buzzerApi.Services.Abstraction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace buzzerApi.Services.Upload
{
    public class UploadService : IUploadService
    {
        public async Task<ICollection<String>> UploadMedia(IOptions<UploadOptions> uploadOptions, IOptions<ConnectionOptions> connexionOptions, MediaType key, IFormFileCollection files)
        {
            var options = uploadOptions.Value;
            string directoryPath = null;
            string keyUrl = null;
            ICollection<string> allowedExtensions = new List<string>();
            if (key == MediaType.Image)
            {
                keyUrl = "image";
                directoryPath = options.MediaImage;
                allowedExtensions = options.ExtensionsImage.Split(',');
            }
            else if (key == MediaType.Audio)
            {
                keyUrl = "audio";
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
                    string url = null;
                    var fileExt = System.IO.Path.GetExtension(file.FileName).Substring(1);
                    string filename = $"{Guid.NewGuid()}_{file.FileName.Replace(" ", "-")}";
                    if (allowedExtensions.Any(x => x == fileExt))
                    {
                        if (file.Length > 0)
                        {
                            filePath = Path.Combine(directoryPath, filename);
                            filePath = filePath.Replace("\\", "/");
                            using (var fileStream = new FileStream(filePath, FileMode.Create))
                            {
                                await file.CopyToAsync(fileStream);
                            }
                            url = $"{connexionOptions.Value.Api}/{keyUrl}/{filename}";
                            filenames.Add(url);
                        }
                        else
                        {
                            throw new Exception("No content");
                        }
                    }
                    else
                    {
                        throw new Exception("Unreconized media type");
                    }
                }
                return filenames;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
