using TrackWallet.Models;

namespace TrackWallet.DataAccess.Repository.IRepository;

public interface IOccasionRepository : IRepository<Occasion>
{
    void Update(Occasion obj);
}