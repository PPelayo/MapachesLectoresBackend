using MapachesLectoresBackend.Core.Domain.Model.Enums;
using MapachesLectoresBackend.Core.Domain.Model.Vo;

namespace MapachesLectoresBackend.Core.Domain.Services
{
    public interface IImagenService
    {
        public Task<Uri> UploadImageAsync(Stream image, ImagenTypesEnum type, string publicId);

    }
}
