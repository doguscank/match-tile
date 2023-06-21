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
                if (TileBarManager.Instance.tileList.Count > 0)
                {
                    TileType type = TileBarManager.Instance.tileList.First.Value.tileType;

                    foreach (BaseTile tile in TileManager.Instance.GetTilesByTypeFromGrid(type).OrderBy(_ => Guid.NewGuid()).ToList().Take(3))
                    {
                        tile.Unlock();
                        TileManager.Instance.SelectTile(tile);
                    }

                    return;
                }

                TileType randomType = TileManager.Instance.GetRandomTileFromGrid().tileType;
                List<BaseTile> tiles = TileManager.Instance.GetTilesByTypeFromGrid(randomType);

                while (tiles.Count < 3)
                {
                    randomType = TileManager.Instance.GetRandomTileFromGrid().tileType;
                    tiles = TileManager.Instance.GetTilesByTypeFromGrid(randomType);
                }

                foreach (BaseTile tile in tiles.OrderBy(_ => Guid.NewGuid()).ToList().Take(3))
                {
                    tile.Unlock();
                    TileManager.Instance.SelectTile(tile);
                }
            }
        }
    }
}