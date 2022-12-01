using System.Collections.Generic;
using System.IO;
using System.Linq;
using MemoStack.Model;
using MemoStack.UseCase;
using Microsoft.EntityFrameworkCore;

namespace MemoStack.Repository;

public class MemoContext : DbContext, IRepository
{
    private readonly string _dbPath = Path.Join(".", "memo.db");

    public DbSet<MemoModel> MemoModels { get; set; }

    public IEnumerable<MemoModel> GetMemos()
    {
        return MemoModels.ToList();
    }

    public void UpdateMemo(MemoModel model)
    {
        Update(model);
        SaveChanges();
    }

    public void DeleteMemo(MemoModel model)
    {
        Remove(model);
        SaveChanges();
    }

    public bool Exists(MemoModel model)
    {
        return MemoModels.Any(m => m == model);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"Data Source={_dbPath}");
    }
}