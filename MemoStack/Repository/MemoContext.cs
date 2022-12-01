using System.IO;
using MemoStack.UseCase;
using Microsoft.EntityFrameworkCore;

namespace MemoStack.Repository;

public class MemoContext : DbContext
{
    private readonly string _dbPath = Path.Join(".", "memo.db");

    public DbSet<MemoModel> MemoModels { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"Data Source={_dbPath}");
    }
}