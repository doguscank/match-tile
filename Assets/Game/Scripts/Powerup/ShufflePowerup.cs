using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.UI;

using MatchTile.Manager;
using MatchTile.Tile;
using MatchTile.Utils;

namespace MatchTile.Powerup
{
    public class ShufflePowerup : SingletonBase<ShufflePowerup>, IPowerup
    {
        public bool isLocked { get; private set; }

        public void SetLocked(bool isLocked)
        {
            this.isLocked = isLocked;

            GameObject.Find("Canvas").transform.Find("ShufflePowerupButton").GetComponent<Button>().interactable = !isLocked;

            if (isLocked)
                GameObject.Find("Canvas").transform.Find("ShufflePowerupButton").GetComponent<Image>().color = PowerupColors.InactiveButtonColor;
            else
                GameObject.Find("Canvas").transform.Find("ShufflePowerupButton").GetComponent<Image>().color = PowerupColors.ActiveButtonColor;
        }

        public void Activate()
        {
            if (!isLocked)
            {
                List<BaseTile> tiles = TileManager.Instance.GetTiles();
                List<TileType> tileTypes = tiles.Select(tile => tile.tileType).OrderBy(_ => Guid.NewGuid()).ToList();

                for (int i = 0; i < tiles.Count; i++)
                {
                    tiles[i].SetTileType(tileTypes[i]);
                }
            }
        }
    }
}