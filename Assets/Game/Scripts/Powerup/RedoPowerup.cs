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
        public bool isEnabled => TileBarManager.Instance.history.history.Count != 0;

        private void Awake()
        {

        }

        private void Update()
        {

        }

        public void Activate()
        {
            Debug.Log("Activated redo");

            if (isEnabled)
            {
                TileBarManager.Instance.Redo();
            }
        }
    }
}