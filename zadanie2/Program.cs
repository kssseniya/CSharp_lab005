using System.Collections;

class MyList<T> : IEnumerable<T>
{
    private T[] values;
    private int capacity;

    public MyList(params T[] valuesInit)
    {
        values = new T[valuesInit.Length];
        Array.Copy(valuesInit, values, valuesInit.Length);
        capacity = valuesInit.Length;
    }

    public void Add(T value)
    {
        if (capacity == values.Length)
        {
            int newCapacity;
            if (capacity == 0)
            {
                newCapacity = 4;
            }
            else
            {
                newCapacity = capacity * 2;
            }
            var newValues = new T[newCapacity];
            Array.Copy(values, newValues, values.Length);
            values = newValues;
        }
        values[capacity] = value; //Добавляем элемент
        capacity++; //Увеличиваем счетчик
    }

    public T this[int index]
    {
        get
        {
            if (index < 0 || index >= capacity)
                throw new IndexOutOfRangeException("Индекс вне диапазона.");
            return values[index];
        }
        set
        {
            if (index < 0 || index >= capacity)
                throw new IndexOutOfRangeException("Индекс вне диапазона.");
            values[index] = value;
        }
    }

    public int Count
    {
        get { return capacity; }
    }

    public IEnumerator<T> GetEnumerator()
    {
        for (int i = 0; i < capacity; i++)
        {
            yield return values[i];
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Print()
    {
        for (int i = 0; i < capacity; i++)
        {
            Console.Write(values[i] + " ");
        }
        Console.WriteLine();
    }
}

class Program
{
    static void Main(string[] args)
    {
        MyList<int> list = new MyList<int>(1, 2, 3, 4, 5);
        list.Print();

        list.Add(6);
        list.Print();

        Console.WriteLine($"\nОбщее количество элементов {list.Count}");
    }
}