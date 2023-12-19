using TrackWallet.DataAccess.Data;
using TrackWallet.DataAccess.Repository.IRepository;

namespace TrackWallet.DataAccess.Repository;

public class UnitOfWork : IUnitOfWork
{
    public ICategoryRepository Category { get; private set; }
    private ApplicationDbContext _db;
    public UnitOfWork(ApplicationDbContext db)
    {
        _db = db;
        Category = new CategoryRepository(_db);
    }

    public void Save()
    {
        _db.SaveChanges();
    }
}

