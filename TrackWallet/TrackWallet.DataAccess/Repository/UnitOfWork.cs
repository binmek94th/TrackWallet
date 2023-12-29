using TrackWallet.DataAccess.Data;
using TrackWallet.DataAccess.Repository.IRepository;

namespace TrackWallet.DataAccess.Repository;

public class UnitOfWork : IUnitOfWork
{
    public ICategoryRepository Category { get; private set; }
    public IUserSelectedCategoryRepository UserSelectedCategory { get; private set; }
    public IWalletRepository Wallet { get; private set; }
    public IBudgetRepository Budget { get; set; }
    private ApplicationDbContext _db;
    public UnitOfWork(ApplicationDbContext db)
    {
        _db = db;
        Category = new CategoryRepository(_db);
        UserSelectedCategory = new UserSelectedCategoryRepository(_db);
        Wallet = new WalletRepository(_db);
    }

    public void Save()
    {
        _db.SaveChanges();
    }
}

