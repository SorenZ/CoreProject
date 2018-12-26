using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mobin.CoreProject.Admin.Helper;
using Mobin.CoreProject.Core.SSOT;

namespace Mobin.CoreProject.Admin.Controllers
{
    public class UploaderController : Controller
    {
        private readonly FileConfig _fileConfig;

        public UploaderController(FileConfig fileConfig)
        {
            _fileConfig = fileConfig;
        }

        public IActionResult File(string id = "")
        {
            var markup = $@"
                <div dir='rtl'>
                    <form enctype='multipart/form-data' method='post' action=''>

                        <input type='hidden' name='id' value='{id}' />
                        <input type='file' name='file' required='required' id='fileUpload' />
                        <br />
                        <button type='submit' id='submitButton' class='btn btn-success'>آپلود فایل</button>

                    </form>
                </div>
            ";

            return new ContentResult { Content = markup, ContentType = "text/html; charset=utf-8" };
        }

        [HttpPost]
        public IActionResult File(string id, IFormFile file)
        {
            try
            {
                var fileName = FileHelper.SaveFile(file, FileHelper.FileType.File, _fileConfig.PhysicalAddress);

                var markup = $@"
                    <script>
                        window.opener.Regate.RegateFile.set('{id}', '{fileName}');
                        window.close();
                    </script>
                ";

                return new ContentResult { Content = markup, ContentType = "text/html; charset=utf-8" };
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }
        }
    }
}
