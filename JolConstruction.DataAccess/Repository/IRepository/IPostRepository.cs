﻿using JolConstruction.Models;

namespace JolConstruction.DataAccess.Repository.IRepository
{
    public interface IPostRepository : IRepository<Post>
    {
        void Update(Post obj);
    }
}
