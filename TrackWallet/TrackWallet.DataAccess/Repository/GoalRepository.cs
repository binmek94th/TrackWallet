using TrackWallet.DataAccess.Data;
using TrackWallet.DataAccess.Repository.IRepository;
using TrackWallet.Models;

namespace TrackWallet.DataAccess.Repository;

public class GoalRepository : Repository<Goal>, IGoalRepository
{
    private ApplicationDbContext _db;
    
    public GoalRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }
    
    public void Update(Goal obj)
    {
        _db.Goals.Update(obj);

    }
    
}