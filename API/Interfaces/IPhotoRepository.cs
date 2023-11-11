using API.Entities;

namespace API.Interfaces
{
    public interface IPhotoRepository
    {
        void DeletePhoto(Photo photo);
        Task<Photo> GetPhotoById(int id);
    }
}