using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.UI;

using MatchTile.Manager;
using MatchTile.Tile;
using MatchTile.TileBar;
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
            Debug.Log("Activated shuffle");

            if (!isLocked)
            {
                List<IBaseTile> tiles = TileManager.Instance.GetTiles();
                List<Vector3> positions = tiles.Select(tile => tile.GetPosition()).OrderBy(_ => Guid.NewGuid()).ToList();

                for (int i = 0; i < tiles.Count; i++)
                {
                    tiles[i].GetGameobject().GetComponent<TileMovement>().MoveToPosition(positions[i]);
                    tiles[i].ResetParents();
                    tiles[i].ResetChildren();

                    tiles[i].FindChildren();
                }

                // for (int i = 0; i < tiles.Count; i++)
                // {
                //     tiles[i].FindChildren();
                // }

                for (int i = 0; i < tiles.Count; i++)
                {
                    tiles[i].CheckParentsExist();
                }
            }
        }
    }
}