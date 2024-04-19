
namespace WebEcommerce.Areas.Admin.Services
{
    public class UploadFile : IUploadFile
    {
        private static string UPLOAD_FOLDER_NAME = "uploads";
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UploadFile(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<string> UploadSingle(IFormFile file)
        {
            string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, UPLOAD_FOLDER_NAME);

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            string fileName = Path.GetFileName(file.FileName);
            string filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = File.Create(filePath))
            {
                await file.CopyToAsync(stream);
            }

            return $"/{UPLOAD_FOLDER_NAME}/{fileName}";
        }
    }
}
