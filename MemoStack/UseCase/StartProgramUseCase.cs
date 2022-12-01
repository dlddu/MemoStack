using System.Collections.Generic;
using System.Linq;
using MemoStack.Model;

namespace MemoStack.UseCase;

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