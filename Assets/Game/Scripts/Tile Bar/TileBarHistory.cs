using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MatchTile.Powerup;
using MatchTile.Tile;
using MatchTile.Utils;

namespace MatchTile.TileBar
{
    public class TileBarHistory
    {
        public LinkedList<TileBarHistoryNode> history { get; private set; }

        public TileBarHistory()
        {
            history = new LinkedList<TileBarHistoryNode>();
        }

        public void AddHistoryNode(IBaseTile tile)
        {
            history.AddLast(new TileBarHistoryNode(tile));
        }

        private TileBarHistoryNode FindMatchingNodeByTile(IBaseTile tile)
        {
            var current = history.First;

            while (current != null && current.Value.tile != tile)
            {
                current = current.Next;
            }

            return current.Value;
        }

        public bool RemoveHistoryNodeByTile(IBaseTile tile)
        {
            var match = FindMatchingNodeByTile(tile);

            if (match != null)
            {
                history.Remove(match);
                return true;
            }

            return false;
        }

        public TileBarHistoryNode Redo()
        {
            if (history.Count != 0)
            {
                var temp = history.Last.Value;
                history.RemoveLast();
                return temp;
            }

            return null;
        }

        public void Reset()
        {
            history = new LinkedList<TileBarHistoryNode>();
        }
    }
}