using System.Collections.Generic;

using UnityEngine;

namespace MatchTile.Tile
{
    public interface IBaseTile
    {
        bool isLocked { get; }
        bool isInBar { get; }
        int tileId { get; }
        bool isDestroyed { get; }
        TileType tileType { get; }
        SpriteRenderer lockedTint { get; }

        List<BaseTile> parents { get; }
        List<BaseTile> children { get; }

        void Lock();
        void Unlock();
        void SetDestroyed();
        void SetTileId(int id);
        void SetTileType(TileType type);
        void SetMovedToBar();
        void SetRemovedFromBar();
        bool CheckParentsExist();
        void AddParent(BaseTile parent);
        bool RemoveParent(BaseTile parent);
        void AddChild(BaseTile child);
        bool RemoveChild(BaseTile child);
        void ResetParents();
        void ResetChildren();
        void FindChildren();
        GameObject GetGameobject();
        Vector3 GetPosition();
    }
}
