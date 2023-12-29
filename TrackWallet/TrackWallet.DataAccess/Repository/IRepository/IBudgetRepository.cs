using TrackWallet.Models;

namespace TrackWallet.DataAccess.Repository.IRepository;

public interface IBudgetRepository : IRepository<Budget>
{
    void Update(Budget obj);
}