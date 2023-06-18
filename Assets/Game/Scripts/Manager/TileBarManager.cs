using System.Collections;

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
                current.Value.GetGameobject().GetComponent<TileMovement>().MoveToPosition(TileBarPositions.Positions7[i]);
            }
        }

        public Vector3 AddTile(IBaseTile tile)
        {
            // Get tile type id
            // Add tile by sorting by tile type id
            // Get index of new tile
            int idx = InsertTile(tile);
            tile.GetGameobject().GetComponent<TileMovement>().MoveToPosition(TileBarPositions.Positions7[idx]);
            // Move tiles with greated tile type id by one to right
            MoveTilesRightFromIndex(idx);
            // Move tile from game screen to tile bar
            // Check if 3 match exists
            // Remove tiles if match exists
            // Check if isFull
            // Game over if isFull = true
            throw new System.NotImplementedException();
        }

        // Function: Move tiles to right starting from index
    }
}
