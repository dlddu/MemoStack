using System.Collections.ObjectModel;
using System.Windows.Input;
using MemoStack.Model;

namespace MemoStack;

public interface IMainViewModel
{
    public ObservableCollection<MemoModel> ObservableStack { get; }
    public MemoModel PoppedMemoModel { get; }
    public ICommand CreateMemoCommand { get; }
    public ICommand DeleteMemoCommand { get; }
}