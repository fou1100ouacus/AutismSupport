using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Service.Abstracts;

namespace Service.Implementations
{
    public class FileService : IFileService
    {
        #region Fileds
        private readonly IWebHostEnvironment _webHostEnvironment;
        #endregion
        #region Constructors
        public FileService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        #endregion
        #region Handle Functions
        // public async Task<string> UploadImage(string Location, IFormFile file)
        // {
        //     var path = _webHostEnvironment.WebRootPath+"/"+Location+"/";
        //     var extention = Path.GetExtension(file.FileName);
        //     var fileName = Guid.NewGuid().ToString().Replace("-", string.Empty)+extention;
        //     if (file.Length > 0)
        //     {
        //         try
        //         {
        //             if (!Directory.Exists(path))
        //             {
        //                 Directory.CreateDirectory(path);
        //             }
        //             using (FileStream filestreem = File.Create(path+fileName))
        //             {
        //                 await file.CopyToAsync(filestreem);
        //                 await filestreem.FlushAsync();
        //                 return $"/{Location}/{fileName}";
        //             }
        //         }
        //         catch (Exception)
        //         {
        //             return "FailedToUploadImage";
        //         }
        //     }
        //     else
        //     {
        //         return "NoImage";
        //     }
        // }
     public async Task<string> UploadImage(string Location, IFormFile file)
{
    // 1. تأمين مسار الـ wwwroot حتى لو عاد السيرفر بقيمة Null
    var rootPath = _webHostEnvironment.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
    
    // 2. دمج المسار بشكل قياسي يتوافق مع بيئة السيرفر
    var path = Path.Combine(rootPath, Location);
    
    var extention = Path.GetExtension(file.FileName);
    var fileName = Guid.NewGuid().ToString().Replace("-", string.Empty) + extention;
    
    if (file.Length > 0)
    {
        try
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            
            // 3. دمج مسار الملف النهائي بشكل سليم
            var filePath = Path.Combine(path, fileName);
            
            using (FileStream filestreem = File.Create(filePath))
            {
                await file.CopyToAsync(filestreem);
                await filestreem.FlushAsync();
                return $"/{Location}/{fileName}";
            }
        }
        catch (Exception ex)
        {
            // يمكنكِ تسجيل ex.Message في الـ Log إن أردتِ
            return "FailedToUploadImage";
        }
    }
    else
    {
        return "NoImage";
    }
}
     
     
        #endregion
    }
}
