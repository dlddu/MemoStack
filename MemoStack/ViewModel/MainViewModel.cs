using System.Collections.Generic;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using MemoStack.Repository;
using MemoStack.UseCase;

namespace MemoStack.ViewModel;

public class MainViewModel : IMainViewModel
{
    private readonly Stack<MemoModel> _stack;

    public MainViewModel()
    {
        using var repository = new MemoContext();
        _stack = new StartProgramUseCase(repository).Invoke();
        PoppedMemoModel = _stack.TryPop(out var memoModel) ? memoModel : new MemoModel(string.Empty, 0);
    }

    public ICommand SaveMemoCommand { get; } = new RelayCommand<MemoModel>(model =>
    {
        if (model == null) return;
        using var repository = new MemoContext();
        var useCase = new SaveMemoUseCase(repository);
        useCase.Invoke(model);
    });

    public MemoModel PoppedMemoModel { get; }
    public ICommand CreateMemoCommand { get; }
    public ICommand DeleteMemoCommand { get; }
}