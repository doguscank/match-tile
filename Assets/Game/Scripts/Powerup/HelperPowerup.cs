using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.UI;

using Random = UnityEngine.Random;

using MatchTile.Manager;
using MatchTile.Tile;
using MatchTile.Utils;

namespace MatchTile.Powerup
{
    public class HelperPowerup : SingletonBase<HelperPowerup>, IPowerup
    {
        public bool isLocked { get; private set; } = true;

        public void SetLocked(bool isLocked)
        {
            this.isLocked = isLocked;

            GameObject.Find("Canvas").transform.Find("HelperPowerupButton").GetComponent<Button>().interactable = !isLocked;

            if (isLocked)
                GameObject.Find("Canvas").transform.Find("HelperPowerupButton").GetComponent<Image>().color = PowerupColors.InactiveButtonColor;
            else
                GameObject.Find("Canvas").transform.Find("HelperPowerupButton").GetComponent<Image>().color = PowerupColors.ActiveButtonColor;
        }

        public void Activate()
        {
            if (!isLocked)
            {
                TileType randomType = TileManager.Instance.GetRandomTile().tileType;
                List<IBaseTile> tiles = TileManager.Instance.GetTilesByType(randomType);

                foreach (IBaseTile tile in tiles.OrderBy(_ => Guid.NewGuid()).ToList().Take(3))
                {
                    tile.Unlock();
                    TileManager.Instance.SelectTile(tile);
                }
            }
        }
    }
}