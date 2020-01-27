using System;

class Solution
{

    static int[,] MagicSquare2(int n)
    {
        var square = new int[n, n];
        var bitmap = new bool[n * n + 1];

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

    static bool ValidUpTo(int n, int position, int[,] square)
    {
        var mc = (n * ((n * n) + 1)) / 2;

        for (int r = 0; r < n; r++)
        {
            if (position == (r + 1) * n - 1)
            {
                int sum = 0;
                for (int c = 0; c < n; c++) { sum += square[r, c]; }
                return (sum == mc);
            }
        }

        for (int c = 0; c < n; c++)
        {
            if (position == n * (n - 1) + c)
            {
                int sum = 0;
                for (int r = 0; r < n; r++) { sum += square[r, c]; }
                return (sum == mc);
            }
        }

        return true;
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
        if (position == n * n)
        {
            if (Done(n, square))
            {
                Console.WriteLine("Found!");
                Print(n, square);
                return true;
            }
        }

        if (position >= n * n) return false;

        var x = position / n;
        var y = position % n;

        for (int val = 1; val <= n * n; val++)
        {
            if (bitmap[val]) continue;

            square[x, y] = val;
            bitmap[val] = true;

            if (ValidUpTo(n, position, square) && MagicSquare2(n, position + 1, bitmap, square))
            {
                return true;
            }

            square[x, y] = 0;
            bitmap[val] = false;
        }

        return false;
    }


    static void Main(string[] args)
    {
        MagicSquare m = new MagicSquare(4);
        m.solve(0);
        m.Print();

        //m = new MagicSquare(3);
        //m.count(0);
        //Console.WriteLine("There are " + m.total + " possible squares.");

        var m2 = MagicSquare2(3);

        Print(3, m2);

        var m4 = MagicSquare2(4);

        Print(4, m4);
    }




    public class MagicSquare
    {
        int[,] square;
        bool[] used;
        int n;
        int magicSum;
        public int total = 0;

        public MagicSquare(int n)
        {
            square = new int[n, n];
            this.n = n;
            used = new bool[n * n + 1];
            magicSum = n * (n * n + 1) / 2;
        }

        // handles only rows and columns
        bool validUpTo(int step)
        {
            for (int r = 0; r < n; r++)
            {
                if (step == (r + 1) * n - 1)
                {
                    int sum = 0;
                    for (int c = 0; c < n; c++) { sum += square[r, c]; }
                    return (sum == magicSum);
                }
            }

            for (int c = 0; c < n; c++)
            {
                if (step == n * (n - 1) + c)
                {
                    int sum = 0;
                    for (int r = 0; r < n; r++) { sum += square[r, c]; }
                    return (sum == magicSum);
                }
            }

            return true;
        }

        bool isValid()
        {
            int sumD1 = 0;
            int sumD2 = 0;
            for (int i = 0; i < n; i++)
            {
                int sumR = 0;
                int sumC = 0;
                sumD1 += square[i, i];
                sumD2 += square[i, n - i - 1];
                for (int j = 0; j < n; j++)
                {
                    sumR += square[i, j];
                    sumC += square[j, i];
                }
                if (sumR != magicSum || sumC != magicSum) { return false; }
            }

            // diagonals
            return (sumD1 == magicSum && sumD2 == magicSum);
        }

        public void Print() => Solution.Print(n, square);

        public bool solve(int step)
        {
            if (step == n * n)
            {
                return Done(n, square);
            }

            for (int val = 1; val <= n * n; val++)
            {
                if (used[val]) { continue; }

                used[val] = true;
                square[step / n, step % n] = val;
                if (validUpTo(step) && solve(step + 1))
                {
                    return true;
                }
                square[step / n, step % n] = 0;
                used[val] = false;
            }

            return false;
        }

        public void count(int step)
        {
            if (step == n * n)
            {
                if (isValid())
                {
                    total++;
                    Print();
                }
                return;
            }

            for (int val = 1; val <= n * n; val++)
            {
                if (used[val]) { continue; }

                used[val] = true;
                square[step / n, step % n] = val;
                if (validUpTo(step))
                {
                    count(step + 1);
                }
                square[step / n, step % n] = 0;
                used[val] = false;
            }
        }
    }
}
