using TrackWallet.DataAccess.Data;
using TrackWallet.DataAccess.Repository.IRepository;
using TrackWallet.Models;

namespace TrackWallet.DataAccess.Repository;

public class UnitOfWork : IUnitOfWork
{
    public ICategoryRepository Category { get; private set; }
    public IUserSelectedCategoryRepository UserSelectedCategory { get; private set; }
    public IWalletRepository Wallet { get; private set; }
    public IBudgetRepository Budget { get; set; }
    public IEventRepository Event { get; set; } 

    public IGoalRepository Goal { get; set; }
    public IRecurringTransactionRepository RecurringTransaction { get; }
    public IOccasionRepository Occasion { get; }
    public ILoanAndDebtRepository LoanAndDebt { get; set; }
    public ISharedWalletRepository SharedWallet { get; set; }
    public ITransactionRepository Transaction { get; }
    public INotificationRepository Notification { get; }

    private ApplicationDbContext _db;
    public UnitOfWork(ApplicationDbContext db)
    {
        _db = db;
        Category = new CategoryRepository(_db);
        UserSelectedCategory = new UserSelectedCategoryRepository(_db);
        Wallet = new WalletRepository(_db);
        Budget = new BudgetRepository(_db);
        Event = new EventRepository(_db);
        Goal = new GoalRepository(_db);
        RecurringTransaction = new RecurringTransactionRepository(_db);
        Occasion = new OccasionRepository(_db);
        LoanAndDebt = new LoanAndDebtRepository(_db);
        SharedWallet = new SharedWalletRepository(_db);
        Transaction = new TransactionRepository(_db);
        Notification = new NotificationRepository(_db);
    }

    public void Save()
    {
        _db.SaveChanges();
    }
}

