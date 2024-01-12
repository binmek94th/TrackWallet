using TrackWallet.DataAccess.Data;
using TrackWallet.DataAccess.Repository.IRepository;
using TrackWallet.Models;

namespace TrackWallet.DataAccess.Repository;

public class LoanAndDebtRepository : Repository<LoanAndDebt>, ILoanAndDebtRepository
{
    private ApplicationDbContext _db;
    
    public LoanAndDebtRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }
    
    public void Update(LoanAndDebt obj)
    {
        _db.LoanAndDebts.Update(obj);
    }
    

    
}