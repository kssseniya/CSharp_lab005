public class MyMatrix
{
    private double[,] matrix;
    public int Rows { get; set; }
    public int Cols { get; set; }

    public MyMatrix(int rows, int cols)
    {
        Rows = rows;
        Cols = cols;
        matrix = new double[rows, cols];
        Fill();
    }

    public void Fill()
    {
        Console.Write("Введите минимальное значение для случайных чисел: ");
        double min = double.Parse(Console.ReadLine());
        Console.Write("Введите максимальное значение для случайных чисел: ");
        double max = double.Parse(Console.ReadLine());

        Random random = new Random();
        for (int i = 0; i < Rows; i++)
        {
            for (int j = 0; j < Cols; j++)
            {
                matrix[i, j] = random.NextDouble() * (max - min) + min;
            }
        }
    }

    public void ChangeSize(int newRows, int newCols)
    {
        double[,] newMatrix = new double[newRows, newCols];
        int minRows = Math.Min(Rows, newRows);
        int minCols = Math.Min(Cols, newCols);

        for (int i = 0; i < minRows; i++)
        {
            for (int j = 0; j < minCols; j++)
            {
                newMatrix[i, j] = matrix[i, j];
            }
        }

        FillRandomPart(newMatrix, minRows, minCols, newRows, newCols);

        matrix = newMatrix;
        Rows = newRows;
        Cols = newCols;
    }

    private void FillRandomPart(double[,] matrix, int startRow, int startCol, int endRow, int endCol)
    {
        Console.Write("Введите минимальное значение для случайных чисел (дозаполнение): ");
        double min = double.Parse(Console.ReadLine());
        Console.Write("Введите максимальное значение для случайных чисел (дозаполнение): ");
        double max = double.Parse(Console.ReadLine());

        Random random = new Random();
        for (int i = startRow; i < endRow; i++)
        {
            for (int j = startCol; j < endCol; j++)
            {
                if (matrix[i, j] == 0)
                {
                    matrix[i, j] = random.NextDouble() * (max - min) + min;
                }
            }
        }
    }

    public void ShowPartialy(int startRow, int endRow, int startCol, int endCol)
    {
        // Проверка на корректность индексов
        if (startRow < 0 || endRow >= Rows || startCol < 0 || endCol >= Cols || startRow > endRow || startCol > endCol)
        {
            Console.WriteLine("Некорректные индексы для вывода.");
            return;
        }

        for (int i = startRow; i <= endRow; i++)
        {
            for (int j = startCol; j <= endCol; j++)
            {
                Console.Write($"{matrix[i, j]:F2}\t");
            }
            Console.WriteLine();
        }
    }

    public void Show()
    {
        ShowPartialy(0, Rows - 1, 0, Cols - 1);
    }

    public double this[int row, int col]
    {
        get
        {
            if (row < 0 || row >= Rows || col < 0 || col >= Cols)
            {
                throw new IndexOutOfRangeException("Индекс за пределами матрицы.");
            }
            return matrix[row, col];
        }
        set
        {
            if (row < 0 || row >= Rows || col < 0 || col >= Cols)
            {
                throw new IndexOutOfRangeException("Индекс за пределами матрицы.");
            }
            matrix[row, col] = value;
        }
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        Console.Write("Введите количество строк: ");
        int rows = int.Parse(Console.ReadLine());
        Console.Write("Введите количество столбцов: ");
        int cols = int.Parse(Console.ReadLine());

        MyMatrix myMatrix = new MyMatrix(rows, cols);
        Console.WriteLine("\nИсходная матрица:");
        myMatrix.Show();

        Console.Write("\nВведите новое количество строк: ");
        int newRows = int.Parse(Console.ReadLine());
        Console.Write("Введите новое количество столбцов: ");
        int newCols = int.Parse(Console.ReadLine());

        myMatrix.ChangeSize(newRows, newCols);
        Console.WriteLine("\nИзменённая матрица:");
        myMatrix.Show();

        Console.Write("\nВведите начальную строку для частичного вывода (считая с 1): ");
        int startRowUser = int.Parse(Console.ReadLine());
        Console.Write("Введите конечную строку для частичного вывода (считая с 1): ");
        int endRowUser = int.Parse(Console.ReadLine());
        Console.Write("Введите начальный столбец для частичного вывода (считая с 1): ");
        int startColUser = int.Parse(Console.ReadLine());
        Console.Write("Введите конечный столбец для частичного вывода (считая с 1): ");
        int endColUser = int.Parse(Console.ReadLine());

        myMatrix.ShowPartialy(startRowUser - 1, endRowUser - 1, startColUser - 1, endColUser - 1);

        Console.Write("\nВведите номер строки для доступа к элементу (считая с 1): ");
        int row = int.Parse(Console.ReadLine());
        Console.Write("Введите номер столбца для доступа к элементу (считая с 1): ");
        int col = int.Parse(Console.ReadLine());

        try
        {
            Console.WriteLine($"\nЗначение элемента [{row},{col}]: {myMatrix[row - 1, col - 1]:F2}");
        }
        catch (IndexOutOfRangeException ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }
}