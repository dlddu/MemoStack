namespace MemoStack.UseCase;

public class DeleteMemoUseCase
{
    private readonly IRepository _repository;

    public DeleteMemoUseCase(IRepository repository)
    {
        _repository = repository;
    }

    public void Invoke(MemoModel model)
    {
        if (_repository.Exists(model))
            _repository.DeleteMemo(model);
    }
}