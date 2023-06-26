namespace JolConstruction.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ICategoryRepository Category { get; }
        IPostRepository Post { get; }
        IPostImageRepository PostImage { get; }

        void Save();
    }
}
