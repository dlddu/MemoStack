using System.IO;
using System.Windows;
using MemoStack.Repository;
using Microsoft.EntityFrameworkCore;

namespace MemoStack;

/// <summary>
///     Interaction logic for App.xaml
/// </summary>
public partial class App
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        if (!Directory.Exists(MemoContext.DbDirectory))
            Directory.CreateDirectory(MemoContext.DbDirectory);
        using var dbContext = new MemoContext();
        dbContext.Database.Migrate();
    }
}