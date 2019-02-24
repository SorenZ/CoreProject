using System;
using System.Collections.Generic;
using System.IO;
using HeyRed.Mime;
using Microsoft.AspNetCore.Http;


namespace Mobin.CoreProject.AuthAdmin.Helper
{
    public static class FileHelper
    {
        public enum FileType
        {
            Image,
            File,
        }

        // path should be absolute physical address, eg: C:\Repository\
        public static string SaveFile(IFormFile file, FileType fileType, string path)
        {
            if (file.Length <= 0)
            {
                throw new Exception("there is no content in uploaded file.");
            }

            var date = DateTime.Now;
            var relativePath = $"{fileType}/{date.Year}/{date.Month}/{date.Day}/";
            var folderPath = Path.Combine(path, relativePath);

            var fileName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(file.FileName);

            Directory.CreateDirectory(folderPath);
            var filepath = Path.Combine(folderPath, fileName);

            using (var fileStream = new FileStream(filepath, FileMode.Create))
            {
                file.CopyTo(fileStream);
            }

            //Check Mime Type
            var allowedExtensions = new List<string>
            {
                "image/png",
                "image/tiff",
                "image/jpeg",
                "image/bmp",
                "image/gif",
            };

            if (fileType == FileType.File)
            {
                allowedExtensions.Add("application/msword");
                allowedExtensions.Add("application/zip");
                allowedExtensions.Add("application/x-rar-compressed");
                allowedExtensions.Add("application/pdf");
                allowedExtensions.Add("application/vnd.openxmlformats-officedocument.wordprocessingml.document");
                allowedExtensions.Add("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            }

            var mimeType = MimeGuesser.GuessMimeType(filepath);

            if (allowedExtensions.Contains(mimeType))
                return Path.Combine(relativePath, fileName);

            try
            {
                File.Delete(filepath);
            }
            catch (IOException)
            {

            }

            throw new Exception("Invalid File Type");
        }
    }
}