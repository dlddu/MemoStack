using System.Collections.Generic;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MemoStack.Model;
using MemoStack.Repository;
using MemoStack.UseCase;

namespace MemoStack.ViewModel;

public class MainViewModel : ObservableObject, IMainViewModel
{
    private readonly Stack<MemoModel> _stack;
    private ICommand? _createMemoCommand;
    private ICommand? _deleteMemoCommand;
    private MemoModel _poppedMemoModel;
    private ICommand? _saveMemoCommand;

    public MainViewModel()
    {
        using var repository = new MemoContext();
        _stack = new StartProgramUseCase(repository).Invoke();
        _poppedMemoModel = _stack.TryPop(out var memoModel) ? memoModel : new MemoModel(string.Empty, 0);
    }

    public ICommand SaveMemoCommand =>
        _saveMemoCommand ??= new RelayCommand(() =>
        {
            using var repository = new MemoContext();
            var useCase = new SaveMemoUseCase(repository);
            useCase.Invoke(PoppedMemoModel);
        });

    public MemoModel PoppedMemoModel
    {
        get => _poppedMemoModel;
        private set => SetProperty(ref _poppedMemoModel, value);
    }

    public ICommand CreateMemoCommand =>
        _createMemoCommand ??= new RelayCommand(() =>
        {
            _stack.Push(PoppedMemoModel);
            using var repository = new MemoContext();
            var useCase = new CreateMemoUseCase(repository);
            PoppedMemoModel = useCase.Invoke(_stack);
        });

    public ICommand DeleteMemoCommand =>
        _deleteMemoCommand ??= new RelayCommand(() =>
        {
            using var repository = new MemoContext();
            var useCase = new DeleteMemoUseCase(repository);
            useCase.Invoke(PoppedMemoModel);
            PoppedMemoModel = _stack.TryPop(out var model) ? model : new MemoModel(string.Empty, 0);
        });
}