using Match3.Core;

namespace Match3.Core
{
    public class Board
    {
        private readonly Grid<TileType> grid;

        public int Width => grid.Width;
        public int Height => grid.Height;

        public Board(int width, int height)
        {
            grid = new Grid<TileType>(width, height);
        }

        public TileType GetCell(int x, int y)
        {
            return grid.GetCell(x, y);
        }

        public void SetCell(int x, int y, TileType val)
        {
            grid.SetCell(x, y, val);
        }

        public bool IsEmpty(int x, int y)
        {
            return GetCell(x, y) == TileType.None;
        }
    }
}

