using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MatchTile.Tile
{
    public interface IBaseTile
    {
        bool IsLocked { get; set; }
        TileType TileType { get; set; }

        List<IBaseTile> TopTiles { get; set; }
        List<IBaseTile> BottomTiles { get; set; }

        void Lock();
        void Unlock();

    }
}
