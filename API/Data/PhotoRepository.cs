using API.Entities;
using API.Interfaces;

namespace API.Data
{
    public class PhotoRepository : IPhotoRepository
    {
        private readonly DataContext _context;

        public PhotoRepository(DataContext context)
        {
            this._context = context;
            
        }

        public void DeletePhoto(Photo photo)
        {
            _context.Photos.Remove(photo);
        }

        public async Task<Photo> GetPhotoById(int id)
        {
            return await _context.Photos.FindAsync(id);
        }
    }
}