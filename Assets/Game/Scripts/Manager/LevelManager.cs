using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using MatchTile.Level;
using MatchTile.Tile;
using MatchTile.Utils;

namespace MatchTile.Manager
{
    public class LevelManager : SingletonBase<LevelManager>
    {
        public LevelData allLevelsData { get; private set; }

        private void Awake()
        {
            LoadLevels();
        }

        private void Start()
        {
            
        }

        private void Update()
        {
            
        }

        public void ResetLevel()
        {
            
        }

        public void LoadLevel(int levelId)
        {
            ResetLevel();

            if (levelId >= allLevelsData.levelData.Count)
                throw new System.IndexOutOfRangeException($"Level with Id {levelId} is not available!");

            var level = allLevelsData.levelData[levelId];

            foreach (var tileData in level.tileData)
            {
                TileManager.Instance.SpawnTileAt(tileData.position, tileData.type);
            }
        }

        public void LoadLevels()
        {
            TextAsset levelsJson = Resources.Load<TextAsset>("Level/Levels");

            if (levelsJson != null)
            {
                allLevelsData = JsonUtility.FromJson<LevelData>(levelsJson.text);
            }
            else
            {
                allLevelsData = new LevelData(new List<LevelDatum>());
            }
        }

        public void SaveLevel()
        {
#if UNITY_EDITOR
            List<TileDatum> tileData = new List<TileDatum>();

            foreach (IBaseTile tile in TileManager.Instance.GetTiles())
            {
                tileData.Add(new TileDatum(tile.GetPosition(), tile.tileType));
            }

            LevelDatum level = new LevelDatum(allLevelsData.levelData.Count, tileData);
            allLevelsData.levelData.Add(level);

            string json = JsonUtility.ToJson(allLevelsData);
            System.IO.File.WriteAllText(Application.dataPath + "/Resources/Level/Levels.json", json);
#endif
        }
    }
}
