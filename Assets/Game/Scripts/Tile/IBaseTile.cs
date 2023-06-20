using System.Collections.Generic;

using UnityEngine;

namespace MatchTile.Tile
{
    public interface IBaseTile
    {
        bool isLocked { get; }
        bool isInBar { get; }
        int tileId { get; }
        TileType tileType { get; }
        SpriteRenderer lockedTint { get; }

        List<IBaseTile> parents { get; }
        List<IBaseTile> children { get; }

        void Lock();
        void Unlock();
        void SetTileId(int id);
        void SetTileType(TileType type);
        void SetMovedToBar();
        void SetRemovedFromBar();
        bool CheckParentsExist();
        void AddParent(IBaseTile parent);
        bool RemoveParent(IBaseTile parent);
        void AddChild(IBaseTile child);
        bool RemoveChild(IBaseTile child);
        GameObject GetGameobject();
        Vector3 GetPosition();
    }
}
