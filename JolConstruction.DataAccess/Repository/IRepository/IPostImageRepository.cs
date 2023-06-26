using JolConstruction.Models;

namespace JolConstruction.DataAccess.Repository.IRepository
{
    public interface IPostImageRepository :IRepository<PostImage>
    {
        void Update(PostImage postImage);
    }
}
