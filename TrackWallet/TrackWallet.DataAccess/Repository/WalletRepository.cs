using TrackWallet.DataAccess.Data;
using TrackWallet.DataAccess.Repository.IRepository;
using TrackWallet.Models;

namespace TrackWallet.DataAccess.Repository;

public class WalletRepository : Repository<Wallet>, IWalletRepository
{
    private ApplicationDbContext _db;
    
    public WalletRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }
    
    public void Update(Wallet obj)
    {
        _db.Wallets.Update(obj);
    }
    

    
}