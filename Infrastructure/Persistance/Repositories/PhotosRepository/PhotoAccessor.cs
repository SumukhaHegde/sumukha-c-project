using Application.Photos.DTO;
using Application.Photos.Interface;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Npgsql;

namespace Infrastructure.Persistance.Repositories.PhotosRepository
{
    public class PhotoAccessor : IPhotoAccessor
    {
        private readonly Cloudinary _cloudinary;
        private readonly IDBConnectionFactory _dbConnectionFactory;
        private readonly string connectionString;
        public PhotoAccessor(IOptions<CloudinarySetting> config, IDBConnectionFactory dBConnectionFactory)
        {
            var account = new Account(
                config.Value.CloudName,
                config.Value.ApiKey,
                config.Value.Apisecret
                );
            _cloudinary = new Cloudinary(account);
            _dbConnectionFactory = dBConnectionFactory;
            connectionString = _dbConnectionFactory.CreateDBConnection(Enum.ConnectionType.ReadOnly);

        }

        public async Task<string> DeletePhoto(string publicId)
        {
            var deleteParams = new DeletionParams(publicId);
            var result = await _cloudinary.DestroyAsync(deleteParams);
            return result.Result == "ok" ? result.Result : null;
        }

        public async Task<PhotosUploadResult> UploadPhoto(IFormFile file, int userId)
        {
            var query = "INSERT INTO photo(url,isMain, userid,photoid) VALUES (@Url,@IsMain, @UserId, @PhotoId)";

            if (file.Length > 0)
            {
                await using var stream = file.OpenReadStream();
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    Transformation = new Transformation().Width(500).Height(500).Crop("fill")
                };
                var uploadResult = await _cloudinary.UploadAsync(uploadParams);
                if (uploadResult.Error != null)
                {
                    throw new Exception(uploadResult.Error.Message);
                }

                using (var connection = new NpgsqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    var users = await connection.ExecuteAsync(query, new
                    {
                        Url = uploadResult.Url.ToString(),
                        IsMain = true,
                        UserId = userId,
                        PhotoId = uploadResult.PublicId,
                    });
                    connection.Close();
                }

                return new PhotosUploadResult
                {
                    PublicId = uploadResult.PublicId,
                    Url = uploadResult.SecureUrl.ToString()
                };
            }
            return null;
        }
    }
}
