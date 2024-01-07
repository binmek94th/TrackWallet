using TrackWallet.Models;

namespace TrackWallet.DataAccess.Repository.IRepository;

public interface IGoalRepository : IRepository<Goal>
{
    void Update(Goal obj);
}