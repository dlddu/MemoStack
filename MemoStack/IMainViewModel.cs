using System.Windows.Input;
using MemoStack.UseCase;

namespace MemoStack;

public interface IMainViewModel
{
    public MemoModel PoppedMemoModel { get; }
    public ICommand CreateMemoCommand { get; }
    public ICommand DeleteMemoCommand { get; }
}