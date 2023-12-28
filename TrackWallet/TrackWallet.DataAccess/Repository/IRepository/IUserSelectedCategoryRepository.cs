using TrackWallet.Models;

namespace TrackWallet.DataAccess.Repository.IRepository;

public interface IUserSelectedCategoryRepository : IRepository<UserSelectedCategory>
{
    void Update(UserSelectedCategory obj);
}