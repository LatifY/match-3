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
            var (x,y) = grid.GetCoordinates(i);
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
}