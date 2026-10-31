using System;
using System.Collections.Generic;

namespace Match3.Core
{
    public class Grid<T>
    {
        public int Width { get; }
        public int Height { get; }
        readonly T[] cells;

        static readonly int[,] dirs4 = { { 1, 0 }, {-1, 0}, {0, 1}, {0, -1} };
        static readonly int[,] dirs8 = { { 1, 0 }, {-1, 0}, {0, 1}, {0, -1},
                                        { 1, 1 }, { -1, 1 }, { 1, -1 }, { -1, -1 }};

        public Grid(int width, int height)
        {
            if(width <= 0 || height <= 0) throw new ArgumentOutOfRangeException(nameof(width) + " + " + nameof(height));
            this.Width = width;
            this.Height = height;
            cells = new T[width * height];
        }

        public (int x, int y) GetCoordinates(int idx)
        {
            if(idx >= cells.Length || idx < 0) throw new ArgumentOutOfRangeException(nameof(idx));
            int x = idx % Width;
            int y = idx / Width;
            return (x, y);
        }
        
        public int GetIndex(int x, int y)
        {
            if(!IsInside(x, y)) throw new ArgumentOutOfRangeException(nameof(x) + " + " + nameof(y));
            return  y * Width + x;
        }

        public bool IsInside(int x, int y)
        {
            return 0 <= x && x < Width && 0 <= y && y < Height;
        }

        public T GetCell(int x, int y)
        {
            return cells[GetIndex(x, y)];
        }

        public bool TryGetCell(int x, int y, out T value)
        {
            value = default;
            if(IsInside(x, y))
            {
                value = GetCell(x, y);
                return true;
            }
            return false;
        }

        public void SetCell(int x, int y, T value)
        {
            cells[GetIndex(x, y)] = value;
        }

        public List<T> GetNeighbors4(int x, int y)
        {
            return GetNeighbors(x,y, dirs4);
        }

        public List<T> GetNeighbors8(int x, int y)
        {
            return GetNeighbors(x,y, dirs8);
        }

        private List<T> GetNeighbors(int x, int y, int[,] dirs)
        {
            List<T> list = new List<T>();
            for (int i = 0; i < dirs.GetLength(0); i++)
            {
                int nx = x + dirs[i, 0];
                int ny = y + dirs[i, 1];

                if(IsInside(nx, ny)) list.Add(GetCell(nx, ny));
            }
            return list;
        }
    }

}