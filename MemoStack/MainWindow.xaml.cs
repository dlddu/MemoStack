using System;
using System.Windows;
using MemoStack.ViewModel;

namespace MemoStack;

/// <summary>
///     Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow
{
    private readonly MainViewModel _viewModel = new();

    public MainWindow()
    {
        InitializeComponent();

        DataContext = _viewModel;
    }

    private void UIElement_OnLostFocus(object sender, RoutedEventArgs e)
    {
        _viewModel.SaveMemoCommand.Execute(null);
    }

    protected override void OnClosed(EventArgs e)
    {
        base.OnClosed(e);
        _viewModel.SaveMemoCommand.Execute(null);
    }
}