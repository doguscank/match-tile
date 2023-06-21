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
        public SpriteRenderer lockedTint { get; private set; }

        // Tile lists
        public List<IBaseTile> parents { get; private set; }
        public List<IBaseTile> children { get; private set; }

        private void Awake()
        {
            parents = new List<IBaseTile>();
            children = new List<IBaseTile>();

            lockedTint = transform.GetChild(0).GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            FindChildren();
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

            GetComponent<SpriteRenderer>().color = TileColors.colors[(int)type];
        }

        public void Lock()
        {
            isLocked = true;
            lockedTint.enabled = isLocked;
        }

        public void Unlock()
        {
            isLocked = false;
            lockedTint.enabled = isLocked;
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
            if (!parents.Contains(parent))
                parents.Add(parent);

            Lock();
        }

        public bool RemoveParent(IBaseTile parent)
        {
            bool result = parents.Remove(parent);
            CheckParentsExist();

            return result;
        }

        public void ResetParents()
        {
            parents = new List<IBaseTile>();
        }

        public void ResetChildren()
        {
            children = new List<IBaseTile>();
        }

        public bool CheckParentsExist()
        {
            if (parents.Count == 0)
            {
                Unlock();
                return false;
            }

            Lock();
            return true;
        }

        public void AddChild(IBaseTile child)
        {
            if (!children.Contains(child))
                children.Add(child);
        }

        public bool RemoveChild(IBaseTile child)
        {
            return children.Remove(child);
        }

        public GameObject GetGameobject()
        {
            return gameObject;
        }

        public Vector3 GetPosition()
        {
            return transform.position;
        }

        public void FindChildren()
        {
            Bounds overlapBounds = new Bounds(transform.position, transform.localScale);
            Collider2D[] colliders = Physics2D.OverlapBoxAll(overlapBounds.center, overlapBounds.size, 0f);

            foreach (var collider in colliders)
            {
                if (collider.gameObject.transform.position.z == transform.position.z + 1)
                {
                    AddChild(collider.gameObject.GetComponent<IBaseTile>());
                    collider.gameObject.GetComponent<BaseTile>().AddParent(this);
                }
            }
        }
    }
}
