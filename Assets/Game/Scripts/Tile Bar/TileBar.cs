using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using MatchTile.Tile;
using MatchTile.Utils;

namespace MatchTile.TileBar
{
    public class TileBar : SingletonBase<TileBar>
    {
        LinkedList<IBaseTile> tileList;

        public const int tileBarLength = 7;
        public int numberOfTiles => tileList.Count;
        public bool isFull => numberOfTiles == tileBarLength;

        private void Awake()
        {
            tileList = new LinkedList<IBaseTile>();
        }

        private void Start()
        {
            
        }

        private void Update()
        {
            
        }

        public void AddTile(IBaseTile tile)
        {
            // Get tile type id
            // Add tile by sorting by tile type id
            // Get index of new tile
            // Move tiles with greated tile type id by one to right
            // Move tile from game screen to tile bar
            // Check if 3 match exists
            // Remove tiles if match exists
            // Check if isFull
            // Game over if isFull = true
        }

        // Function: Move tiles to right starting from index
    }
}
