using System.Collections.Generic;

using UnityEngine;

using MatchTile.Tile;
using MatchTile.TileBar;
using MatchTile.Utils;

namespace MatchTile.Manager
{
    public class TileBarManager : SingletonBase<TileBarManager>
    {
        LinkedList<IBaseTile> tileList;

        public int tileBarLength { get; private set; } = 7;
        public int numberOfTiles => tileList.Count;
        public bool isFull => numberOfTiles == tileBarLength;

        void Awake()
        {
            tileList = new LinkedList<IBaseTile>();
        }

        void Start()
        {
            
        }

        void Update()
        {
            
        }

        private int InsertTile(IBaseTile tile)
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

        public void RemoveTiles(int index)
        {
            var current = tileList.First;

            for (int i = 0; i < index - 2; i++)
                current = current.Next;

            for (int i = 0; i < 3; i++)
            {
                var temp = current.Next;
                Destroy(current.Value.GetGameobject());
                tileList.Remove(current);
                current = temp;
            }
        }

        public void AddTile(IBaseTile tile)
        {
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
            Debug.Log("Moving from idx: " + idx.ToString());
            MoveTilesRightFromIndex(idx);

            if (CheckMatch(idx))
            {
                Debug.Log("Match found!");

                RemoveTiles(idx);
                MoveTilesLeftFromIndex(idx - 2);
            }


            // Move tile from game screen to tile bar
            // Check if 3 match exists
            // Remove tiles if match exists
            // Check if isFull
            // Game over if isFull = true
        }
    }
}
