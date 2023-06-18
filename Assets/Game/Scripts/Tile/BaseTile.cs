using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MatchTile.Manager;

namespace MatchTile.Tile
{
    public class BaseTile : MonoBehaviour, IBaseTile
    {
        // Tile properties
        public bool isLocked { get; private set; } = false;
        public bool isInBar { get; private set; } = false;
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

        public void SetTileType(TileType type)
        {
            tileType = type;

            Color[] colors = new Color[3];
            colors[0] = Color.red;
            colors[1] = Color.green;
            colors[2] = Color.blue;

            GetComponent<SpriteRenderer>().color = colors[(int)type];
        }

        public void Lock()
        {
            isLocked = true;
        }

        public void Unlock()
        {
            isLocked = false;
        }

        public void SetMovedToBar()
        {
            isInBar = true;
        }

        public void SetRemovedFromBar()
        {
            isInBar = false;
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

        // Level editor
        private void FindChildren()
        {
            throw new System.NotImplementedException();
        }
    }
}
