using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MemoStack.Model;
using MemoStack.Repository;
using MemoStack.UseCase;
using MemoStack.Utility;

namespace MemoStack.ViewModel;

public class MainViewModel : ObservableObject, IMainViewModel
{
    private ICommand? _createMemoCommand;
    private ICommand? _deleteMemoCommand;
    private MemoModel _poppedMemoModel;
    private ICommand? _saveMemoCommand;

    public MainViewModel()
    {
        using var repository = new MemoContext();
        ObservableStack = new ObservableCollection<MemoModel>(new StartProgramUseCase(repository).Invoke());
        _poppedMemoModel = ObservableStack.TryPop(out var model) ? model : new MemoModel(string.Empty, 0);
    }

    public ICommand SaveMemoCommand =>
        _saveMemoCommand ??= new RelayCommand(() =>
        {
            using var repository = new MemoContext();
            var useCase = new SaveMemoUseCase(repository);
            useCase.Invoke(PoppedMemoModel);
        });

    public ObservableCollection<MemoModel> ObservableStack { get; }

    public MemoModel PoppedMemoModel
    {
        get => _poppedMemoModel;
        private set => SetProperty(ref _poppedMemoModel, value);
    }

    public ICommand CreateMemoCommand =>
        _createMemoCommand ??= new RelayCommand(() =>
        {
            ObservableStack.Add(PoppedMemoModel);
            using var repository = new MemoContext();
            var useCase = new CreateMemoUseCase(repository);
            PoppedMemoModel = useCase.Invoke(ObservableStack);
        });

    public ICommand DeleteMemoCommand =>
        _deleteMemoCommand ??= new RelayCommand(() =>
        {
            using var repository = new MemoContext();
            var useCase = new DeleteMemoUseCase(repository);
            useCase.Invoke(PoppedMemoModel);
            PoppedMemoModel = ObservableStack.TryPop(out var model) ? model : new MemoModel(string.Empty, 0);
        });
}