using JolConstruction.Data;
using JolConstruction.DataAccess.Repository.IRepository;

namespace JolConstruction.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;
        public ICategoryRepository Category { get; private set; }
        public IPostRepository Post { get; private set; }
        public IPostImageRepository PostImage { get; private set; }

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Category = new CategoryRepository(_db);
            Post = new PostRepository(_db);
            PostImage = new PostImageRepository(_db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
