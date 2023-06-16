using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MatchTile.Utils;

namespace MatchTile.Manager
{
    public class CoinManager : SingletoneBase<CoinManager>
    {
        public int Coin { get; private set; }

        public void AddCoin(int coin)
        {
            Coin += coin;
        }
    }
}
