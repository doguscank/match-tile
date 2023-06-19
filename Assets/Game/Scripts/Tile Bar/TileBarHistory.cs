using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MatchTile.Tile;
using MatchTile.Utils;

namespace MatchTile.TileBar
{
    public class TileBarHistory : SingletonBase<TileBarHistory>
    {
        public LinkedList<TileBarHistoryNode> history { get; private set; }

        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void AddHistoryNode(IBaseTile tile, Vector3 originalPosition)
        {
            history.AddLast(new TileBarHistoryNode(tile, originalPosition));
        }

        public TileBarHistoryNode RedoHistory()
        {
            var last = history.Last;
            history.RemoveLast();
            return last.Value;
        }
    }
}