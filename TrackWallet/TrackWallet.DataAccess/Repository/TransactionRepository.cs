using TrackWallet.DataAccess.Data;
using TrackWallet.DataAccess.Repository.IRepository;
using TrackWallet.Models;

namespace TrackWallet.DataAccess.Repository;

public class TransactionRepository : Repository<Transaction>, ITransactionRepository
{
    private ApplicationDbContext _db;
    
    public TransactionRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }
    
    public void Update(Transaction obj)
    {
        _db.Transactions.Update(obj);
    }
    

    
}