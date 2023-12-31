using System.Collections.Generic;

using UnityEngine;

namespace MatchTile.Tile
{
    [System.Serializable]
    public class BaseTile : MonoBehaviour, IBaseTile
    {
        // Tile properties
        public bool isLocked { get; private set; } = false;
        public bool isInBar { get; private set; } = false;
        public bool isDestroyed { get; private set; } = false;
        public int tileId { get; private set; }
        public TileType tileType { get; private set; }
        public SpriteRenderer lockedTint { get; private set; }

        // Tile lists
        public List<BaseTile> parents { get; private set; }
        public List<BaseTile> children { get; private set; }

        private void Awake()
        {
            parents = new List<BaseTile>();
            children = new List<BaseTile>();

            lockedTint = transform.GetChild(0).GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            FindChildren();
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
            if (!isLocked)
            {
                isLocked = true;
                lockedTint.enabled = isLocked;
            }
        }

        public void Unlock()
        {
            if (isLocked)
            {
                isLocked = false;
                lockedTint.enabled = isLocked;
            }
        }

        public void SetDestroyed()
        {
            isDestroyed = true;
        }

        public void SetMovedToBar()
        {
            isInBar = true;
        }

        public void SetRemovedFromBar()
        {
            isInBar = false;
        }

        public void AddParent(BaseTile parent)
        {
            if (!parents.Contains(parent))
                parents.Add(parent);

            Lock();
        }

        public bool RemoveParent(BaseTile parent)
        {
            bool result = parents.Remove(parent);
            CheckParentsExist();

            return result;
        }

        public void ResetParents()
        {
            parents.Clear();
        }

        public void ResetChildren()
        {
            children.Clear();
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

        public void AddChild(BaseTile child)
        {
            if (!children.Contains(child))
                children.Add(child);
        }

        public bool RemoveChild(BaseTile child)
        {
            return children.Remove(child);
        }

        public GameObject GetGameobject()
        {
            if (!isDestroyed && gameObject != null)
                return gameObject;
            return null;
        }

        public Vector3 GetPosition()
        {
            return transform.position;
        }

        public void FindChildren()
        {
            Bounds overlapBounds = new Bounds(transform.position, new Vector3(2f, 2f, 1f));
            Collider2D[] colliders = Physics2D.OverlapBoxAll(overlapBounds.center, overlapBounds.size, 0f);

            foreach (var collider in colliders)
            {
                if (collider.gameObject.transform.position.z == transform.position.z + 1)
                {
                    AddChild(collider.gameObject.GetComponent<BaseTile>());
                    collider.gameObject.GetComponent<BaseTile>().AddParent(this);
                }
            }
        }
    }
}
