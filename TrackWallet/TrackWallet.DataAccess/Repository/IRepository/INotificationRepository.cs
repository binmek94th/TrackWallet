using TrackWallet.Models;

namespace TrackWallet.DataAccess.Repository.IRepository;

public interface INotificationRepository : IRepository<Notification>
{
    void Update(Notification obj);
}