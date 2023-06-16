using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MatchTile.Utils;

namespace MatchTile.Manager
{
    public class CoinManager : SingletoneBase<CoinManager>
    {
        public int coin { get; private set; }

        public void AddCoin(int coin)
        {
            this.coin += coin;
        }
    }
}
