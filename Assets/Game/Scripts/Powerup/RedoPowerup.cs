using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MatchTile.Tile;
using MatchTile.TileBar;
using MatchTile.Utils;

namespace MatchTile.Powerup
{
    public class RedoPowerup : MonoBehaviour, IPowerup
    {
        public RedoPowerup()
        {

        }

        public void Activate()
        {
            var lastMove = TileBarHistory.Instance.RedoHistory();

            lastMove.tile.GetGameobject().GetComponent<TileMovement>().MoveToPosition(lastMove.originalPosition);
        }
    }
}