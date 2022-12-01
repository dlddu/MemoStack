using System.Collections.Generic;
using MemoStack.Model;

namespace MemoStack.UseCase;

public interface IRepository
{
    IEnumerable<MemoModel> GetMemos();
    void UpdateMemo(MemoModel model);
    void DeleteMemo(MemoModel model);
    bool Exists(MemoModel model);
}