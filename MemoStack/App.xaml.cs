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
        using var dbContext = new MemoContext();
        dbContext.Database.Migrate();
    }
}