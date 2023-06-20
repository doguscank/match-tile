using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using MatchTile.Manager;
using MatchTile.Tile;
using MatchTile.TileBar;
using MatchTile.Utils;

namespace MatchTile.Powerup
{
    public class RedoPowerup : SingletonBase<RedoPowerup>, IPowerup
    {
        public bool isLocked { get; private set; }
        public bool isEnabled => TileBarManager.Instance.history.history.Count != 0;

        public void SetLocked(bool isLocked)
        {
            this.isLocked = isLocked;
        }

        public void Activate()
        {
            Debug.Log("Activated redo");

            if (!isLocked && isEnabled)
            {
                TileBarManager.Instance.Redo();
            }
        }
    }
}