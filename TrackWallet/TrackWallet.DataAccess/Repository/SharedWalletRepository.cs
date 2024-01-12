using TrackWallet.DataAccess.Data;
using TrackWallet.DataAccess.Repository.IRepository;
using TrackWallet.Models;

namespace TrackWallet.DataAccess.Repository;

public class SharedWalletRepository : Repository<SharedWallet>, ISharedWalletRepository
{
    private ApplicationDbContext _db;
    
    public SharedWalletRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }
    
    public void Update(SharedWallet obj)
    {
        _db.SharedWallets.Update(obj);
    }
    

    
}