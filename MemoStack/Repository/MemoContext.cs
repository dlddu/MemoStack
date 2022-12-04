using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MemoStack.Model;
using MemoStack.UseCase;
using Microsoft.EntityFrameworkCore;

namespace MemoStack.Repository;

public class MemoContext : DbContext, IRepository
{
#if DEBUG
    public static readonly string DbDirectory = ".";
#else
    public static readonly string DbDirectory =
        Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), nameof(MemoStack));
#endif
    private static readonly string DbPath = Path.Join(DbDirectory, "memo.db");

    public DbSet<MemoModel> MemoModels { get; set; } = null!;

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
        optionsBuilder.UseSqlite($"Data Source={DbPath}");
    }
}