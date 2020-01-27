using System;

class Solution
{

    static int[,] MagicSquare2(int n)
    {
        var square = new int[n, n];
        var bitmap = new bool[n * n];

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                square[i, j] = 0;
            }
        }

        MagicSquare2(n, 0, bitmap, square);

        return square;
    }

    static void Print(int n, int[,] square)
    {
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                Console.Write(square[i, j] + " ");
            }

            Console.WriteLine();
        }

        Console.WriteLine("---------------------");
    }

    static bool Done(int n, int[,] square)
    {
        var mc = (n * ((n * n) + 1)) / 2;

        var diag1sum = 0;
        var diag2sum = 0;

        for (int i = 0; i < n; i++)
        {
            int rowsum = 0;
            int colsum = 0;

            for (int j = 0; j < n; j++)
            {
                if (square[i, j] == 0) return false;

                if (i == j) diag1sum += square[i, j];
                if (i == n - j - 1) diag2sum += square[i, j];

                rowsum += square[i, j];
                colsum += square[j, i];
            }

            if (rowsum != mc || colsum != mc) return false;
        }

        return (diag1sum == mc && diag2sum == mc);
    }

    static bool MagicSquare2(int n, int position, bool[] bitmap, int[,] square)
    {
        if (Done(n, square))
        {
            Console.WriteLine("Found!");
            Print(n, square);
            return true;
        }

        if (position >= n * n) return false;

        var x = position / n;
        var y = position % n;

        for (int i = 1; i <= n * n; i++)
        {
            if (bitmap[i - 1] == false)
            {
                square[x, y] = i;
                bitmap[i - 1] = true;

                if (MagicSquare2(n, position + 1, bitmap, square))
                {
                    return true;
                }

                square[x, y] = 0;
                bitmap[i - 1] = false;
            }            
        }

        return false;
    }


    static void Main(string[] args)
    {
        var m3 = MagicSquare2(3);

        Print(3, m3);

        var m4 = MagicSquare2(4);

        Print(4, m4);
    }
}
