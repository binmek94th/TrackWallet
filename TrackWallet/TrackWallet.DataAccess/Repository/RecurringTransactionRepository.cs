using TrackWallet.DataAccess.Data;
using TrackWallet.DataAccess.Repository.IRepository;
using TrackWallet.Models;

namespace TrackWallet.DataAccess.Repository;

public class RecurringTransactionRepository : Repository<RecurringTransaction>, IRecurringTransactionRepository
{
    private ApplicationDbContext _db;
    
    public RecurringTransactionRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }
    
    public void Update(RecurringTransaction obj)
    {
        _db.RecurringTransactions.Update(obj);
    }
    

    
}