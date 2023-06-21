using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using MatchTile.Manager;
using MatchTile.Tile;
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

            GameObject.Find("Canvas").transform.Find("RedoPowerupButton").GetComponent<Button>().interactable = !isLocked;

            if (isLocked)
                GameObject.Find("Canvas").transform.Find("RedoPowerupButton").GetComponent<Image>().color = PowerupColors.InactiveButtonColor;
            else
                GameObject.Find("Canvas").transform.Find("RedoPowerupButton").GetComponent<Image>().color = PowerupColors.ActiveButtonColor;
        }

        public void Activate()
        {
            if (!isLocked && isEnabled)
            {
                TileBarManager.Instance.Redo();
            }
        }
    }
}