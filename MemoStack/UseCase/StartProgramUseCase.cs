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

    public IEnumerable<MemoModel> Invoke()
    {
        return _repository.GetMemos().OrderBy(model => model.Depth);
    }
}