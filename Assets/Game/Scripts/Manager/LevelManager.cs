using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

using MatchTile.Level;
using MatchTile.Tile;
using MatchTile.Utils;

namespace MatchTile.Manager
{
    public class LevelManager : SingletonBase<LevelManager>
    {
        public LevelData allLevelsData { get; private set; }
        public int playerLevel { get; private set; } = 0;

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

        private void OnEnable()
        {
            SceneManager.sceneLoaded += LoadLastLevel;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= LoadLastLevel;
        }

        public void ResetLevel()
        {
            TileBarManager.Instance.Reset();
            TileManager.Instance.Reset();
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

        public void LoadLevel()
        {
            LoadLevel(playerLevel);
        }

        public void LoadLastLevel(Scene scene, LoadSceneMode mode)
        {
            if (scene.name == "GameScreen")
                LoadLevel(playerLevel);
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
            List<TileDatum> tileData = new List<TileDatum>();

            foreach (IBaseTile tile in TileManager.Instance.GetTiles())
            {
                tileData.Add(new TileDatum(tile.GetPosition(), tile.tileType));
            }

            LevelDatum level = new LevelDatum(allLevelsData.levelData.Count, tileData);
            allLevelsData.levelData.Add(level);

            string json = JsonUtility.ToJson(allLevelsData);
            System.IO.File.WriteAllText(Application.dataPath + "/Resources/Level/Levels.json", json);
        }
    }
}
