using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MatchTile.Tile;
using MatchTile.Utils;

namespace MatchTile.TileBar
{
    public class TileBarHistoryNode
    {
        public BaseTile tile;
        public Vector3 originalPosition;

        public TileBarHistoryNode(BaseTile tile)
        {
            this.tile = tile;
            originalPosition = tile.GetPosition();
        }
    }
}