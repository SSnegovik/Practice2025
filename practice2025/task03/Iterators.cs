using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CustomCollection<T> : IEnumerable<T>
{
    private readonly List<T> _items = new();

    public void Add(T item) => _items.Add(item);
    public IEnumerator<T> GetEnumerator() => _items.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public IEnumerable<T> GetReverseEnumerator()
    {
        int index = _items.Count - 1;
        while (index >= 0)
        {
            yield return _items[index];
            index--;
        }
    }

    public static IEnumerable<int> GenerateSequence(int start, int count)
    {
        int index = 0;
        while (index < count) {
            yield return start + index;
            index++;
        }
    }

    public IEnumerable<T> FilterAndSort(Func<T, bool> predicate, Func<T, IComparable> keySelector)
    {
        return _items.Where(predicate).OrderBy(keySelector);
    }
}
