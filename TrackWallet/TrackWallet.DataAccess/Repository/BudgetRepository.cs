using TrackWallet.DataAccess.Data;
using TrackWallet.DataAccess.Repository.IRepository;
using TrackWallet.Models;

namespace TrackWallet.DataAccess.Repository;

public class BudgetRepository : Repository<Budget>, IBudgetRepository
{
    private ApplicationDbContext _db;
    
    public BudgetRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }
    
    public void Update(Budget obj)
    {
        _db.Budgets.Update(obj);
    }
    

    
}