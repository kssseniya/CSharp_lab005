using System.Collections;

class MyDictionary<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
{
    private KeyValuePair<TKey, TValue>[] items;
    private int count;

    public MyDictionary()
    {
        items = new KeyValuePair<TKey, TValue>[4]; //Начальный размер массива
        count = 0;
    }

    public void Add(TKey key, TValue value)
    {
        if (count == items.Length)
        {
            Resize();
        }
        items[count] = new KeyValuePair<TKey, TValue>(key, value);
        count++;
    }

    public TValue this[TKey key]
    {
        get
        {
            for (int i = 0; i < count; i++)
            {
                if (items[i].Key.Equals(key))
                {
                    return items[i].Value;
                }
            }
            throw new KeyNotFoundException("Ключ не найден.");
        }
    }

    public int Count
    {
        get { return count; }
    }

    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
    {
        for (int i = 0; i < count; i++)
        {
            yield return items[i];
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    private void Resize()
    {
        int newSize = items.Length * 2;
        var newItems = new KeyValuePair<TKey, TValue>[newSize];
        Array.Copy(items, newItems, items.Length);
        items = newItems;
    }
}

class Program
{
    static void Main(string[] args)
    {
        MyDictionary<string, int> myDict = new MyDictionary<string, int>();

        myDict.Add("один", 1);
        myDict.Add("два", 2);
        myDict.Add("три", 3);

        Console.WriteLine("Количество элементов: " + myDict.Count);

        Console.WriteLine("\nЗначение по индексу 1: " + myDict["один"]);

        Console.WriteLine("\nПеребор элементов словаря:");
        foreach (var item in myDict)
        {
            Console.WriteLine($"{item.Key}: {item.Value}");
        }
    }
}