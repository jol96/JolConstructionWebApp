using JolConstruction.Data;
using JolConstruction.DataAccess.Repository.IRepository;
using JolConstruction.Models;

namespace JolConstruction.DataAccess.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private ApplicationDbContext _db;
        public CategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Category obj)
        {
            _db.Categorys.Update(obj);
        }
    }
}
