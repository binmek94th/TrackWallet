﻿using TrackWallet.DataAccess.Data;
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
    public IBillAndReminderRepository  BillAndReminder { get; set; }

    public IGoalRepository Goal { get; set; }
    private ApplicationDbContext _db;
    public UnitOfWork(ApplicationDbContext db)
    {
        _db = db;
        Category = new CategoryRepository(_db);
        UserSelectedCategory = new UserSelectedCategoryRepository(_db);
        Wallet = new WalletRepository(_db);
        Budget = new BudgetRepository(_db);
        Event = new EventRepository(_db);
        BillAndReminder = new BillAndReminderRepository(_db);
        Goal = new GoalRepository(_db);
    }

    public void Save()
    {
        _db.SaveChanges();
    }
}

