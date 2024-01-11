using TrackWallet.DataAccess.Data;
using TrackWallet.DataAccess.Repository.IRepository;
using TrackWallet.Models;

namespace TrackWallet.DataAccess.Repository;

public class OccasionRepository : Repository<Occasion>, IOccasionRepository
{
    private ApplicationDbContext _db;
    
    public OccasionRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }
    
    public void Update(Occasion obj)
    {
        _db.Occasions.Update(obj);
    }
    

    
}