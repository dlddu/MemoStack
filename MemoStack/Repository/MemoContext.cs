using System.Collections.Generic;
using System.IO;
using System.Linq;
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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"Data Source={_dbPath}");
    }
}