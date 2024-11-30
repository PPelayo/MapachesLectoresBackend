using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using MapachesLectoresBackend.Core.Domain.Model.Enums;
using MapachesLectoresBackend.Core.Domain.Services;

namespace MapachesLectoresBackend.Core.Data.Services
{
    public class CloudinaryImagenService : IImagenService
    {

        private readonly Cloudinary _cloudinary;
        private readonly IConfiguration _configuration;

           
        public CloudinaryImagenService(IConfiguration configuration)
        {
            _configuration = configuration;
            _cloudinary = new Cloudinary(_configuration.GetValue<string>("CLOUDINARY_URL"));
            _cloudinary.Api.Secure = true;
        }

        public async Task<Uri> UploadImageAsync(Stream image, ImagenTypesEnum type, string publicId)
        {
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription($"{type}-{publicId}", image),
                PublicId = $"{type}-{publicId}",
                Format = "webp"
            };

            var result = await _cloudinary.UploadAsync(uploadParams);

            return result.SecureUrl;
        }
    }
}
