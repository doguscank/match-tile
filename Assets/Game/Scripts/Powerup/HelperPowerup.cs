using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

using Random = UnityEngine.Random;

using MatchTile.Manager;
using MatchTile.Tile;
using MatchTile.Utils;

namespace MatchTile.Powerup
{
    public class HelperPowerup : SingletonBase<HelperPowerup>, IPowerup
    {
        public bool isLocked { get; private set; }

        public void SetLocked(bool isLocked)
        {
            this.isLocked = isLocked;
        }

        public void Activate()
        {
            int maxValue = (int)Enum.GetValues(typeof(TileType)).Cast<TileType>().Max();
            TileType randomType = (TileType)(Random.Range(0, maxValue));
            List<IBaseTile> tiles = TileManager.Instance.GetTilesByType(randomType);

            foreach (IBaseTile tile in tiles.OrderBy(_ => Guid.NewGuid()).ToList().Take(3))
            {
                TileManager.Instance.SelectTile(tile);
            }
        }
    }
}