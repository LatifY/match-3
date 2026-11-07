using System;
using NUnit.Framework;

using Match3.Core;
using UnityEditor.VersionControl;

public class BoardGeneratorTests
{
    const int W = 7, H = 9;

    [TestCase(2)]
    [TestCase(6)]
    public void ColorCount_Throws(int colorCount)
    {
        Assert.Throws<ArgumentException>(() => new BoardGenerator(new Random(), colorCount));
    }


    [TestCase(3)]
    [TestCase(4)]
    [TestCase(5)]
    public void SeedMatch_Check(int colorCount)
    {
        for (int i = 0; i < 1000; i++)
        {
            Random rnd = new Random(i);
            BoardGenerator bg = new BoardGenerator(rnd, colorCount);
            Board board = bg.GenerateBoard(W, H);

            for (int y = 0; y < H; y++)
            {
                for (int x = 0; x < W; x++)
                {
                    if(x > 1)
                    {
                        TileType l1 = board.GetCell(x, y), l2 = board.GetCell(x -1, y), l3 = board.GetCell(x - 2, y);
                        // Assert.IsFalse(tiles[0] == tiles[1] && tiles [0] == tiles[2], $"MATCH ERROR: seed({i})   (x: {x}, y: {y})");   strings created every time
                        if(l1 == l2 && l1 == l3) Assert.Fail($"MATCH ERROR: seed({i})   (x: {x}, y: {y})"); // check first, more optimized
                    }
                    if(y > 1)
                    {
                        TileType b1 = board.GetCell(x, y), b2 = board.GetCell(x, y - 1), b3 = board.GetCell(x, y - 2);
                        if(b1 == b2 && b1 == b3) Assert.Fail($"MATCH ERROR: seed({i})   (x: {x}, y: {y})");
                    }
                }
            }
        }
    }

    [TestCase(123)]
    [TestCase(321)]
    [TestCase(2005)]
    [TestCase(109267109)]
    public void SameSeedSameBoard_Check(int seed)
    {
        Board b1 = new BoardGenerator(new Random(seed), 5).GenerateBoard(W, H);
        Board b2 = new BoardGenerator(new Random(seed), 5).GenerateBoard(W, H);

        for (int y = 0; y < H; y++)
        {
            for (int x = 0; x < W; x++)
            {
                if(b1.GetCell(x, y) != b2.GetCell(x, y)) Assert.Fail($"MATCH ERROR: seed({seed})   (x: {x}, y: {y})"); 
            }
        }
    }

    [TestCase(5,7, 125)]
    [TestCase(7,9, 136)]
    [TestCase(5,5, 1390246)]
    [TestCase(100, 100, 136126)]
    public void TileTypeNone_Check(int w, int h, int seed)
    {
        Board b1 = new BoardGenerator(new Random(seed), 5).GenerateBoard(w, h);

        for (int y = 0; y < h; y++)
        {
            for (int x = 0; x < w; x++)
            {
                if(b1.GetCell(x, y) == TileType.None) Assert.Fail($"TILETYPE NONE ERROR: seed({seed}) (w: {w}, h: {h})  (x: {x}, y: {y})");
            }
        }
    }

    [TestCase(3)]
    [TestCase(4)]
    public void TileTypeBounded_Correct(int colorCount)
    {
        Board board = new BoardGenerator(new Random(), colorCount).GenerateBoard(W, H);
        for (int y = 0; y < H; y++)
        {
            for (int x = 0; x < W; x++)
            {
                if((int)board.GetCell(x, y) > colorCount ) Assert.Fail($"TILETYPE NONE ERROR: (x: {x}, y: {y})");
            }
        }
    }
}