using System.Collections.Generic;

namespace MatchTile.Level
{
    [System.Serializable]
    public class LevelData
    {
        public List<LevelDatum> levelData;

        public LevelData(List<LevelDatum> levelData)
        {
            this.levelData = levelData;
        }
    }
}