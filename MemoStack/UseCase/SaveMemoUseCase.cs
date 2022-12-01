namespace MemoStack.UseCase;

public class SaveMemoUseCase
{
    private readonly IRepository _repository;

    public SaveMemoUseCase(IRepository repository)
    {
        _repository = repository;
    }

    public void Invoke(MemoModel model)
    {
        _repository.UpdateMemo(model);
    }
}