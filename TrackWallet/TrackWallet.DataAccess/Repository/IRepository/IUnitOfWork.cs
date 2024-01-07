namespace TrackWallet.DataAccess.Repository.IRepository;

public interface IUnitOfWork
{
    ICategoryRepository Category { get; }
    IUserSelectedCategoryRepository UserSelectedCategory { get; }
    IWalletRepository Wallet { get; }
    IBudgetRepository Budget { get;  }
    IEventRepository Event { get; }
    IBillAndReminderRepository BillAndReminder { get; }
    IGoalRepository Goal { get; }
    void Save();
}