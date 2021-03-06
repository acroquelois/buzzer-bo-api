﻿using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace buzzerApi.Middlewares
{
    public class FileAudioMiddleware
    {
        private readonly RequestDelegate _next;

        public FileAudioMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IConfiguration config)
        {
            if (string.IsNullOrEmpty(context.Request.Path.ToString().Replace("/audio/", "")))
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                return;
            }

            string path = $"{config.GetSection("Upload")["MediaAudio"]}/{context.Request.Path.ToString().Replace("/audio/", "")}";

            if (!System.IO.File.Exists(path))
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                return;
            }

            using (var stream = System.IO.File.OpenRead(path))
            {
                string contentType = MimeTypeMap.List.MimeTypeMap.GetMimeType(Path.GetExtension(path)).FirstOrDefault();
                context.Response.ContentType = contentType;
                await stream.CopyToAsync(context.Response.Body);
            }
        }
    }

    public static class FileAudioMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomFileAudioService(this IApplicationBuilder builder, PathString path)
        {
            return builder.MapWhen(c => c.Request.Path.StartsWithSegments(path), b => b.UseMiddleware<FileAudioMiddleware>());
        }
    }
}