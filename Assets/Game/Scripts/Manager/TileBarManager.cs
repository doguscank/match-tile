using System.Collections.Generic;

using UnityEngine;

using MatchTile.Tile;
using MatchTile.TileBar;
using MatchTile.Utils;

namespace MatchTile.Manager
{
    [DefaultExecutionOrder(2)]
    public class TileBarManager : SingletonBase<TileBarManager>
    {
        public LinkedList<BaseTile> tileList { get; private set; }
        public TileBarHistory history { get; private set; }
        public int tileBarLength { get; private set; } = 7;

        public int numberOfTiles => tileList.Count;
        public bool isFull => numberOfTiles == tileBarLength;

        void Awake()
        {
            tileList = new LinkedList<BaseTile>();
            history = new TileBarHistory();
        }

        void Start()
        {
            
        }

        void Update()
        {
            
        }

        public void Reset()
        {
            var current = tileList.First;

            while (current != null)
            {
                var go = current.Value.GetGameobject();
                if (go != null)
                {
                    current.Value.SetDestroyed();
                    Destroy(go);
                }
            }

            tileList = new LinkedList<BaseTile>();
            history = new TileBarHistory();
        }

        private int InsertTile(BaseTile tile)
        {
            var current = tileList.First;
            int idx = 0;

            if (current == null || (int)current.Value.tileType > (int)tile.tileType)
            {
                tileList.AddFirst(tile);
                return idx;
            }

            idx ++;

            while (current.Next != null && (int)current.Next.Value.tileType <= (int)tile.tileType)
            {
                current = current.Next;
                idx ++;
            }

            tileList.AddAfter(current, tile);

            return idx;
        }

        public void MoveTilesRightFromIndex(int index)
        {
            var current = tileList.First;

            for (int i = 0; i < index; i++)
                current = current.Next;

            for (int i = index; i < tileBarLength; i++)
            {
                if (current == null)
                    break;
                
                current.Value.GetGameobject().GetComponent<TileMovement>().MoveToPosition(TileBarPositions.Positions7[i]);
                current = current.Next;
            }
        }

        public void MoveTilesLeftFromIndex(int index)
        {
            var current = tileList.First;

            for (int i = 0; i < index; i++)
                current = current.Next;

            // 4 tiles can be moved at max (7 tiles total - 3 tiles removed = 4 tiles left)
            for (int i = 0; i < 4; i++)
            {                
                if (current == null)
                    break;

                current.Value.GetGameobject().GetComponent<TileMovement>().MoveToPosition(TileBarPositions.Positions7[i + index]);
                current = current.Next;
            }
        }

        public bool CheckMatch(int index)
        {
            var current = tileList.First;

            for (int i = 0; i < index - 2; i++)
            {
                current = current.Next;
            }

            for (int i = 0; i < 2; i++)
            {
                if (current.Next == null || !current.Value.tileType.Equals(current.Next.Value.tileType))
                {
                    return false;
                }

                current = current.Next;
            }

            return true;
        }

        public void RemoveMatchedTiles(int index)
        {
            var current = tileList.First;

            for (int i = 0; i < index - 2; i++)
                current = current.Next;

            for (int i = 0; i < 3; i++)
            {
                var temp = current.Next;
                TileManager.Instance.RemoveTile(current.Value);
                current.Value.SetDestroyed();
                Destroy(current.Value.GetGameobject());
                tileList.Remove(current);
                current = temp;
            }
        }

        public void AddTile(BaseTile tile)
        {
            history.AddHistoryNode(tile);
            
            // Get where the tile is inserted
            int idx = InsertTile(tile);

            foreach (var child in tile.children)
            {
                child.RemoveParent(tile);
                child.CheckParentsExist();
            }

            // Set tile inserted
            tile.SetMovedToBar();
            tile.GetGameobject().GetComponent<TileMovement>().MoveToPosition(TileBarPositions.Positions7[idx]);
            // Move tiles with greater tile type id by one to right
            MoveTilesRightFromIndex(idx);

            if (CheckMatch(idx))
            {
                history.Reset();

                RemoveMatchedTiles(idx);
                MoveTilesLeftFromIndex(idx - 2);

                return;
            }
        }

        private int RemoveTile(BaseTile tile)
        {
            int idx = 0;
            var current = tileList.First;

            while (current != null && current.Value != tile)
            {
                idx ++;
                current = current.Next;
            }

            return idx;
        }

        public void Redo()
        {
            // Remove from history and tile list
            var lastMove = history.Redo();
            int index = RemoveTile(lastMove.tile);
            tileList.Remove(lastMove.tile);
            MoveTilesLeftFromIndex(index);

            lastMove.tile.SetRemovedFromBar();

            foreach (var child in lastMove.tile.children)
            {
                child.AddParent(lastMove.tile);
                child.CheckParentsExist();
            }

            lastMove.tile.GetGameobject().GetComponent<TileMovement>().MoveToPosition(lastMove.originalPosition);
        }
    }
}
