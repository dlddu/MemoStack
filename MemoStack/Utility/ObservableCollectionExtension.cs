using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace MemoStack.Utility;

internal static class ObservableCollectionExtension
{
    internal static bool TryPop<T>(this ObservableCollection<T> collection, [MaybeNullWhen(false)] out T item)
    {
        item = default;
        if (!collection.Any()) return false;
        item = collection.Last();
        collection.Remove(item);
        return true;
    }
}