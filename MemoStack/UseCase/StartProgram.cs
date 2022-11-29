using System.Collections.Generic;
using System.Linq;

namespace MemoStack.UseCase;

public interface IRepository
{
    IEnumerable<MemoModel> GetMemos();
}

public class MemoModel
{
    public MemoModel(string text, int depth)
    {
        Text = text;
        Depth = depth;
    }

    public string Text { get; set; }
    internal int Depth { get; set; }
}

public class StartProgramUseCase
{
    private readonly IRepository _repository;

    public StartProgramUseCase(IRepository repository)
    {
        _repository = repository;
    }

    public Stack<MemoModel> Invoke()
    {
        var memos = _repository.GetMemos().ToList();
        var stack = new Stack<MemoModel>(memos.Count);
        foreach (var memo in memos.OrderBy(memo => memo.Depth)) stack.Push(memo);

        return stack;
    }
}