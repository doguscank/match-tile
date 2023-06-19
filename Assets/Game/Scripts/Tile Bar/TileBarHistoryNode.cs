using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MatchTile.Tile;
using MatchTile.Utils;

namespace MatchTile.TileBar
{
    public class TileBarHistoryNode
    {
        public IBaseTile tile;
        public Vector3 originalPosition;

        public TileBarHistoryNode(IBaseTile tile, Vector3 originalPosition)
        {
            this.tile = tile;
            this.originalPosition = originalPosition;
        }
    }
}