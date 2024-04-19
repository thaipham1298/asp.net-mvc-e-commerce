namespace WebEcommerce.Areas.Admin.Services
{
    public interface IUploadFile
    {
        Task<string> UploadSingle(IFormFile file);
    }
}
