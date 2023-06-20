using System;
using System.Collections.Generic;

using UnityEngine;

using MatchTile.Tile;

namespace MatchTile.Level
{
    [System.Serializable]
    public class LevelDatum
    {
        public int levelId;
        public List<TileDatum> tileData;

        public LevelDatum(int levelId, List<TileDatum> tileData)
        {
            this.levelId = levelId;
            this.tileData = tileData;
        }
    }
}