using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MatchTile.Tile
{
    public class BaseTile : IBaseTile
    {
        public bool m_IsLocked { get; private set; }
        public TileType m_TileType { get; private set; }
        public Vector3 m_GridPosition { get; private set; }


        public List<IBaseTile> m_TopTiles { get; private set; }
        public List<IBaseTile> m_BottomTiles { get; private set; }

        public BaseTile(TileType tileType, Vector3 gridPosition)
        {
            m_IsLocked = true;
            m_TileType = tileType;
            m_GridPosition = gridPosition;
            m_TopTiles = new List<IBaseTile>();
            m_BottomTiles = new List<IBaseTile>();
        }

        public void Lock()
        {
            m_IsLocked = true;
        }

        public void Unlock()
        {
            m_IsLocked = false;
        }

        public bool CheckParents()
        {
            throw new System.NotImplementedException();
        }

        public void MoveTileToGridPosition(Vector3 gridPosition)
        {
            throw new System.NotImplementedException();
        }
    }
}
