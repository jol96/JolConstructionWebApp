using JolConstruction.Data;
using JolConstruction.DataAccess.Repository.IRepository;
using JolConstruction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JolConstruction.DataAccess.Repository
{
    public class PostImageRepository : Repository<PostImage>, IPostImageRepository
    {
        private ApplicationDbContext _db;
        public PostImageRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(PostImage postImage)
        {
            _db.PostImages.Update(postImage);
        }
    }
}
