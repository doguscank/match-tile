using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MatchTile.Tile;

namespace MatchTile.Grid
{
    public class TileIdGrid
    {
        private int[,,] grid;

        public int GetTileIdAt(int x, int y, int h)
        {
            return grid[x, y, h];
        }
    }
}
