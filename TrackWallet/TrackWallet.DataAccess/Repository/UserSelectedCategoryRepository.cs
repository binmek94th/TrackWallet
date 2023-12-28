using TrackWallet.DataAccess.Data;
using TrackWallet.DataAccess.Repository.IRepository;
using TrackWallet.Models;

namespace TrackWallet.DataAccess.Repository;

public class UserSelectedCategoryRepository : Repository<UserSelectedCategory>, IUserSelectedCategoryRepository
{
    private ApplicationDbContext _db;
    
    public UserSelectedCategoryRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }
    
    public void Update(UserSelectedCategory obj)
    {
        _db.UserSelectedCategories.Update(obj);
    }
    

    
}