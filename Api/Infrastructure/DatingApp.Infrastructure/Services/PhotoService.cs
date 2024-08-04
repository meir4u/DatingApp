using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using DatingApp.Domain.Services;
using DatingApp.Infrastructure.Params;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace DatingApp.Infrastructure.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly IOptions<CloudinarySettings> _configuration;
        private readonly Cloudinary _cloudinary;

        public PhotoService(IOptions<CloudinarySettings> configuration)
        {
            _configuration = configuration;

            var acc = new Account
                (
                    configuration.Value.CloudName,
                    configuration.Value.ApiKey,
                    configuration.Value.ApiSecret
                );
            _cloudinary = new Cloudinary(acc);
        }
        public async Task<ImageUploadResult> AddPhotoAsync(IFormFile file)
        {
            var uploadResult = new ImageUploadResult();

            if(file.Length > 0)
            {
                using var stream = file.OpenReadStream();
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    Transformation = new Transformation().Height(500).Width(500).Crop("fill").Gravity("face"),
                    Folder = "da-net7",
                };

                uploadResult = await _cloudinary.UploadAsync(uploadParams);
            }

            return uploadResult;
        }

        public async Task<DeletionResult> DeletePhotoAsync(string publicId)
        {
            var deleteParams = new DeletionParams(publicId);

            return await _cloudinary.DestroyAsync(deleteParams);
        }
    }
}
