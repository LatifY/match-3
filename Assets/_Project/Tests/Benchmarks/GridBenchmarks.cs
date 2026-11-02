using System;
using NUnit.Framework;

using Match3.Core;
using System.Diagnostics;

public class GridBenchmarks
{
    const int W = 500;

    private void ScanArray(String message, Action scan)
    {
        scan(); // warmup
        TestContext.Out.WriteLine("TEST FOR : " + message);
        var sw = Stopwatch.StartNew();
        for(int i = 0; i < 100; i++) scan();
        sw.Stop();
        double ts = sw.Elapsed.TotalMilliseconds;
        TestContext.Out.Write("100 RUN: " + ts + "ms");
    }

    [Test,Explicit]
    public void Scan_OneBlockGrid_500x500()
    {
        var arr = new int[W, W];
        Random rnd = new Random(42);
        for (int y = 0; y < W; y++)
        {
            for (int x = 0; x < W; x++)
            {
                arr[y, x] = rnd.Next(0, 100);
            }
        }
        long sum = 0;

        ScanArray("OneBlockGrid( int[,] )", () =>
        {
            long _sum = 0;
            for (int y = 0; y < W; y++)
            {
                for (int x = 0; x < W; x++)
                {
                    _sum += arr[y, x];
                }
            }
            sum = _sum;
        });
        TestContext.Out.Write("     SUMMATION(RANDOM): " + sum + "\n");
    }

    [Test,Explicit]
    public void Scan_2DArray_500x500()
    {
        var arr = new int[W][];
        for (int i = 0; i < W; i++)
        {
            arr[i] = new int[W];
        }

        Random rnd = new Random(42);
        for (int y = 0; y < W; y++)
        {
            for (int x = 0; x < W; x++)
            {
                arr[y][x] = rnd.Next(0, 100);
            }
        }
        long sum = 0;
        ScanArray("2DArray( int[][] )", () =>
        {
            long _sum = 0;
            for (int y = 0; y < W; y++)
            {
                for (int x = 0; x < W; x++)
                {
                    _sum += arr[y][x];
                }
            }
            sum = _sum;
        });
        TestContext.Out.Write("     SUMMATION(RANDOM): " + sum + "\n");
    }

    [Test,Explicit]
    public void Scan_FlatArray_500x500()
    {
        var arr = new int[W * W];
        Random rnd = new Random(42);
        for (int y = 0; y < W; y++)
        {
            for (int x = 0; x < W; x++)
            {
                arr[y * W + x] = rnd.Next(0, 100);
            }
        }
        long sum = 0;
        ScanArray("FlatArray( int[] )", () =>
        {
            long _sum = 0;
            for (int y = 0; y < W; y++)
            {
                for (int x = 0; x < W; x++)
                {
                    _sum += arr[y * W + x];
                }
            }
            sum = _sum;
        });
        TestContext.Out.Write("     SUMMATION(RANDOM): " + sum + "\n");
    }

    [Test,Explicit]
    public void Scan_Grid_500x500()
    {
        Grid<int> grid = new Grid<int>(W, W);
        Random rnd = new Random(42);
        for (int y = 0; y < W; y++)
        {
            for (int x = 0; x < W; x++)
            {
                grid.SetCell(x, y, rnd.Next(0, 100));
            }
        }
        long sum = 0;
        ScanArray("Our Grid( new Grid<int> )", () =>
        {
            long _sum = 0;
            for (int y = 0; y < W; y++)
            {
                for (int x = 0; x < W; x++)
                {
                    _sum += grid.GetCell(x, y);
                }
            }
            sum = _sum;
        });
        TestContext.Out.Write("     SUMMATION(RANDOM): " + sum + "\n");
    }
}
