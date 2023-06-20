using JolConstruction.Data;
using JolConstruction.DataAccess.Repository.IRepository;
using JolConstruction.Models;

namespace JolConstruction.DataAccess.Repository
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        private ApplicationDbContext _db;
        public PostRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Post obj)
        {
            _db.Posts.Update(obj);
        }
    }
}
