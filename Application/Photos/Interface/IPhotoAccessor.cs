using Application.Photos.DTO;
using Microsoft.AspNetCore.Http;

namespace Application.Photos.Interface
{
    public interface IPhotoAccessor
    {
        Task<PhotosUploadResult> UploadPhoto(IFormFile file, int userId);
        Task<string> DeletePhoto(string publicId);
    }
}
