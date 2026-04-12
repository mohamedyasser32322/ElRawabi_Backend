namespace ElRawabi_Backend.Services.Interface
{
    public interface IImageService
    {
        Task<List<string>> UploadImagesAsync(List<IFormFile> files, string folder);
        Task<bool> DeleteImageAsync(string imageUrl);
    }
}
