using System;
using NUnit.Framework;

using Match3.Core;

public class BoardTests
{
    const int W = 7, H = 9;

    [Test]
    public void BoardCreated_NullFirst()
    {
        Board board = new Board(W, H);
        for (int i = 0; i < H; i++)
        {
            for (int j = 0; j < W; j++)
            {
                Assert.IsTrue(board.IsEmpty(j, i));
            }
        }
    }

    [Test]
    public void BoardSetGet_AreEqual()
    {
        Board board = new Board(W, H);
        board.SetCell(2, 3, TileType.Red);
        Assert.IsTrue(board.GetCell(2, 3) == TileType.Red);
    }

    [Test]
    public void BoardWidthHeight_Check()
    {
        Board board = new Board(W, H);
        Assert.IsTrue(board.Width == W);
        Assert.IsTrue(board.Height == H);
    }


    [TestCase(W, 0)]
    [TestCase(0, H)]
    [TestCase(-1, 0)]
    [TestCase(0, -1)]
    public void BoardExceptionByGrid_Check(int x, int y)
    {
        var board = new Board(W, H);
        Assert.Throws<ArgumentOutOfRangeException>(() => board.GetCell(x, y));
    }
}