using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MatchTile.Tile
{
    public interface IBaseTile
    {
        bool m_IsLocked { get; }
        TileType m_TileType { get; }
        Vector3 m_GridPosition { get; } 

        List<IBaseTile> m_TopTiles { get; }
        List<IBaseTile> m_BottomTiles { get; }

        void Lock();
        void Unlock();
        bool CheckParents();
        void MoveTileToGridPosition(Vector3 gridPosition);
    }
}
