using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MatchTile.Tile
{
    public interface IBaseTile
    {
        bool isLocked { get; }
        int tileId { get; }
        TileType tileType { get; }
        Vector3 gridPosition { get; } 

        List<IBaseTile> topTiles { get; }
        List<IBaseTile> bottomTiles { get; }

        void Lock();
        void Unlock();
        void SetTileId(int id);
        bool CheckParents();
        void AddParent(IBaseTile parent);
        bool RemoveParent(IBaseTile parent);
        void AddChild(IBaseTile child);
        bool RemoveChild(IBaseTile child);
        GameObject GetGameobject();
    }
}
