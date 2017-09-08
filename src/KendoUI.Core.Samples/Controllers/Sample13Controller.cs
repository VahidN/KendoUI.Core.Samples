using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using KendoUI.Core.Samples.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KendoUI.Core.Samples.Controllers
{
    public class Sample13Controller : Controller
    {
        public IActionResult Index()
        {
            return View(); // shows the page.
        }
    }

    public class KendoEditorFilesController : Controller
    {
        //مسیر پوشه فایل‌ها
        protected string FilesFolder = "files";

        protected string KendoFileType = "f";
        protected string KendoDirType = "d";

        protected readonly IHostingEnvironment HostingEnvironment;
        public KendoEditorFilesController(IHostingEnvironment hostingEnvironment)
        {
            HostingEnvironment = hostingEnvironment;
        }

        [HttpPost]
        public KendoFile CreateFolder(string name, string path)
        {
            //تمیز سازی امنیتی
            name = Path.GetFileName(name);
            path = GetSafeDirPath(path);
            var dirToCreate = Path.Combine(path, name);

            Directory.CreateDirectory(dirToCreate);

            return new KendoFile
            {
                Name = name,
                Type = KendoDirType
            };
        }

        [HttpPost]
        public IActionResult DestroyFile(string name, string path)
        {
            //تمیز سازی امنیتی
            name = Path.GetFileName(name);
            path = GetSafeDirPath(path);

            var pathToDelete = Path.Combine(path, name);

            var attr = System.IO.File.GetAttributes(pathToDelete);
            if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
            {
                Directory.Delete(pathToDelete, recursive: true);
            }
            else
            {
                System.IO.File.Delete(pathToDelete);
            }

            return Json(new object[0]);
        }

        [HttpGet]
        public IActionResult GetFile(string path)
        {
            path = GetSafeFileAndDirPath(path);
            return PhysicalFile(path, "image/png");
        }

        [HttpGet]
        public IEnumerable<KendoFile> GetFilesList(string path)
        {
            path = GetSafeDirPath(path);
            var imagesList = new DirectoryInfo(path)
                                .GetFiles()
                                .Select(fileInfo => new KendoFile
                                {
                                    Name = fileInfo.Name,
                                    Size = fileInfo.Length,
                                    Type = KendoFileType
                                }).ToList();

            var foldersList = new DirectoryInfo(path)
                                .GetDirectories()
                                .Select(directoryInfo => new KendoFile
                                {
                                    Name = directoryInfo.Name,
                                    Type = KendoDirType
                                }).ToList();

            return imagesList.Union(foldersList);
        }

        [HttpPost]
        public async Task<KendoFile> UploadFile(IFormFile file, string path)
        {
            //تمیز سازی امنیتی
            var name = Path.GetFileName(file.FileName);
            path = GetSafeDirPath(path);
            var pathToSave = Path.Combine(path, name);

            using (var fileStream = new FileStream(pathToSave, FileMode.Create))
            {
                await file.CopyToAsync(fileStream).ConfigureAwait(false);
            }

            return new KendoFile
            {
                Name = name,
                Size = file.Length,
                Type = KendoFileType
            };
        }

        protected string GetSafeDirPath(string path)
        {
            // path = مسیر زیر پوشه‌ی وارد شده
            if (string.IsNullOrWhiteSpace(path))
            {
                return Path.Combine(HostingEnvironment.WebRootPath, FilesFolder);
            }

            //تمیز سازی امنیتی
            path = Path.GetDirectoryName(path);
            path = Path.Combine(HostingEnvironment.WebRootPath, FilesFolder, path);
            return path;
        }

        protected string GetSafeFileAndDirPath(string path)
        {
            // path = مسیر فایل و زیر پوشه‌ی وارد شده

            //تمیز سازی امنیتی
            var name = Path.GetFileName(path);
            var dir = string.Empty;
            if (!string.IsNullOrWhiteSpace(path))
            {
                dir = Path.GetDirectoryName(path);
            }

            path = Path.Combine(HostingEnvironment.WebRootPath, FilesFolder, dir, name);
            return path;
        }
    }

    public class KendoEditorImagesController : KendoEditorFilesController
    {
        public KendoEditorImagesController(IHostingEnvironment hostingEnvironment) : base(hostingEnvironment)
        {
            // بازنویسی مسیر پوشه‌ی فایل‌ها
            FilesFolder = "images";
        }

        [HttpGet]
        //[ResponseCache(Duration = 3600, VaryByQueryKeys = new[] { "path" })]
        public IActionResult GetThumbnail(string path)
        {
            //todo: create thumb/ resize image

            path = GetSafeFileAndDirPath(path);
            return PhysicalFile(path, "image/png");
        }
    }
}