using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MatchTile.Utils;

namespace MatchTile.Manager
{
    public class CoinManager : SingletonBase<CoinManager>
    {
        public int coins { get; private set; }
        public int stars { get; private set; }

        public void AddCoins(int coins)
        {
            this.coins += coins;
        }

        public void AddStar()
        {
            stars ++;
        }
    }
}
