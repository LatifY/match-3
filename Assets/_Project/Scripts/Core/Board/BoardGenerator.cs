using System;
using Match3.Core;


namespace Match3.Core
{
    public class BoardGenerator
    {
        private Random random;
        private int colorCount;

        public BoardGenerator(Random random, int colorCount)
        {
            if (colorCount < 3 || colorCount > 5) throw new ArgumentException("Colorcount should be between 3 - 5", nameof(colorCount));
            this.random = random;
            this.colorCount = colorCount;
        }

        public Board GenerateBoard(int width, int height)
        {
            Board board = new Board(width, height);
            
            for (int y = 0; y < board.Height; y++)
            {
                for (int x = 0; x < board.Width; x++)
                {
                    int type = random.Next() % colorCount + 1;        //1 2 3 4 5    colorcount=5
                    TileType leftCheck = TileType.None, bottomCheck = TileType.None;

                    if (x > 1)
                    {
                        TileType l1 = board.GetCell(x - 1, y), l2 = board.GetCell(x - 2, y);
                        leftCheck = l1 == l2 ? l1 : TileType.None;
                    }
                    if (y > 1)
                    {
                        TileType b1 = board.GetCell(x, y - 1), b2 = board.GetCell(x, y - 2);
                        bottomCheck = b1 == b2 ? b1 : TileType.None;
                    }

                    while ((TileType)type == leftCheck || (TileType)type == bottomCheck)
                    {
                        type = random.Next() % colorCount + 1;
                    }
                    board.SetCell(x, y, (TileType)type);
                }
            }
            return board;
        }
    }
}