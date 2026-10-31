using System;
using System.Collections.Generic;
using NUnit.Framework;

using Match3.Core;

public class GridTests
{
    [Test]
    public void GetIndex_Grid()
    {
        int w = 7, h = 9;
        Grid<int> grid = new Grid<int>(w, h);
        for (int i = 0; i < w * h; i++)
        {
            var (x, y) = grid.GetCoordinates(i);
            Assert.AreEqual(i, grid.GetIndex(x, y));
        }
    }

    [TestCase(-1, 0, ExpectedResult = false)]
    [TestCase(0, -1, ExpectedResult = false)]
    [TestCase(7, 0, ExpectedResult = false)]
    [TestCase(0, 9, ExpectedResult = false)]
    [TestCase(0, 0, ExpectedResult = true)]
    [TestCase(0, 8, ExpectedResult = true)]
    [TestCase(6, 0, ExpectedResult = true)]
    [TestCase(6, 8, ExpectedResult = true)]
    public bool IsInside_ReturnsExpected(int x, int y)
    {
        int w = 7, h = 9;
        Grid<int> grid = new Grid<int>(w, h);
        return grid.IsInside(x, y);
    }

    [TestCase(2, 3, ExpectedResult = true)]
    [TestCase(0, 0, ExpectedResult = true)]
    [TestCase(6, 8, ExpectedResult = true)]
    [TestCase(7, 8, ExpectedResult = false)]
    [TestCase(2, 9, ExpectedResult = false)]
    [TestCase(12, 20, ExpectedResult = false)]
    public bool TryGetCell_BoundCheck(int x, int y)
    {
        int w = 7, h = 9;
        Grid<int> grid = new Grid<int>(w, h);
        return grid.TryGetCell(x, y, out int val);
    }

    [Test]
    public void SetCell_ThenGetCell()
    {
        int w = 7, h = 9;
        Grid<int> grid = new Grid<int>(w, h);

        grid.SetCell(3, 3, 5);

        Assert.AreEqual(5, grid.GetCell(3, 3));
    }



    const int W = 7, H = 9;

    [TestCase(0, 9)]
    [TestCase(7, -1)]
    [TestCase(-3, -3)]
    public void Constructor_InvalidSize_Throws(int w, int h)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new Grid<int>(w, h));
    }

    [TestCase(W, 0)]
    [TestCase(0, H)]
    [TestCase(-1, 0)]
    [TestCase(0, -1)]
    public void GetCell_Outside_Throws(int x, int y)
    {
        var grid = new Grid<int>(W, H);
        Assert.Throws<ArgumentOutOfRangeException>(() => grid.GetCell(x, y));
    }

    [TestCase(W, 0)]
    [TestCase(0, H)]
    public void SetCell_Outside_Throws(int x, int y)
    {
        var grid = new Grid<int>(W, H);
        Assert.Throws<ArgumentOutOfRangeException>(() => grid.SetCell(x, y, 1));
    }

    [TestCase(-1)]
    [TestCase(W * H)]
    public void GetCoordinates_OutOfRange_Throws(int index)
    {
        var grid = new Grid<int>(W, H);
        Assert.Throws<ArgumentOutOfRangeException>(() => grid.GetCoordinates(index));
    }

    // ---- neighbors ----
    [TestCase(0, 0, ExpectedResult = 2)]          // corner
    [TestCase(W - 1, 0, ExpectedResult = 2)]      // corner
    [TestCase(0, H - 1, ExpectedResult = 2)]      // corner
    [TestCase(W - 1, H - 1, ExpectedResult = 2)]  // corner
    [TestCase(3, 0, ExpectedResult = 3)]          // bottom edge
    [TestCase(0, 4, ExpectedResult = 3)]          // left edge
    [TestCase(3, 4, ExpectedResult = 4)]          // inner
    public int GetNeighbors4_ReturnsCorrectCount(int x, int y)
    {
        var grid = new Grid<int>(W, H);
        return grid.GetNeighbors4(x, y).Count;
    }

    [TestCase(0, 0, ExpectedResult = 3)]          // corner
    [TestCase(W - 1, 0, ExpectedResult = 3)]      // corner
    [TestCase(0, H - 1, ExpectedResult = 3)]      // corner
    [TestCase(W - 1, H - 1, ExpectedResult = 3)]  // corner
    [TestCase(3, 0, ExpectedResult = 5)]          // bottom edge
    [TestCase(0, 4, ExpectedResult = 5)]          // left edge
    [TestCase(3, 4, ExpectedResult = 8)]          // inner
    public int GetNeighbors8_ReturnsCorrectCount(int x, int y)
    {
        var grid = new Grid<int>(W, H);
        return grid.GetNeighbors8(x, y).Count;
    }
}