using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MatchTile.Manager;

namespace MatchTile.Tile
{
    public class BaseTile : MonoBehaviour, IBaseTile
    {
        // Tile properties
        public bool isLocked { get; private set; }
        public int tileId { get; private set; }
        public TileType tileType { get; private set; }
        public Vector3 gridPosition { get; private set; }

        // Tile lists
        public List<IBaseTile> topTiles { get; private set; }
        public List<IBaseTile> bottomTiles { get; private set; }

        private void Awake()
        {
            topTiles = new List<IBaseTile>();
            bottomTiles = new List<IBaseTile>();

            if (GameManager.Instance.isDebug)
            {

            }
        }

        private void Update()
        {

        }

        public void SetTileId(int id)
        {
            tileId = id;
        }

        public void Lock()
        {
            isLocked = true;
        }

        public void Unlock()
        {
            isLocked = false;
        }

        private void FindChildren()
        {
            throw new System.NotImplementedException();
        }

        public void AddParent(IBaseTile parent)
        {
            topTiles.Add(parent);
        }

        public bool RemoveParent(IBaseTile parent)
        {
            return topTiles.Remove(parent);
        }

        public bool CheckParents()
        {
            throw new System.NotImplementedException();
        }

        public void AddChild(IBaseTile child)
        {
            bottomTiles.Add(child);
        }

        public bool RemoveChild(IBaseTile child)
        {
            return bottomTiles.Remove(child);
        }

        public GameObject GetGameobject()
        {
            return gameObject;
        }
    }
}
