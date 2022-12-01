using System.Collections.Generic;
using MemoStack.Model;

namespace MemoStack.UseCase;

public class CreateMemoUseCase
{
    private readonly IRepository _repository;

    public CreateMemoUseCase(IRepository repository)
    {
        _repository = repository;
    }

    public MemoModel Invoke(Stack<MemoModel> memoModels)
    {
        var peek = memoModels.Peek();
        _repository.UpdateMemo(peek);
        var currentDepth = peek.Depth;
        var newModel = new MemoModel(string.Empty, currentDepth + 1);
        return newModel;
    }
}