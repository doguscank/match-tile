using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

using MatchTile.Level;
using MatchTile.Powerup;
using MatchTile.Tile;
using MatchTile.Utils;

namespace MatchTile.Manager
{
    [DefaultExecutionOrder(-1)]
    public class LevelManager : SingletonBase<LevelManager>
    {
        public LevelData allLevelsData { get; private set; }

        private void Awake()
        {
            LoadLevels();

            if (!GameManager.Instance.editorMode)
                UpdatePowerupButtonStates();
        }

        private void Start()
        {

        }

        private void Update()
        {

        }

        private void OnEnable()
        {
            if (!GameManager.Instance.editorMode)
                SceneManager.sceneLoaded += LoadLastLevel;
        }

        private void OnDisable()
        {
            if (!GameManager.Instance.editorMode)
                SceneManager.sceneLoaded -= LoadLastLevel;
        }

        public void Reset()
        {
            TileBarManager.Instance.Reset();
            TileManager.Instance.Reset();
        }

        public void UpdatePowerupButtonStates()
        {
            if (PlayerDataManager.Instance.playerLevel > 0) RedoPowerup.Instance.SetLocked(false);
            else RedoPowerup.Instance.SetLocked(true);

            if (PlayerDataManager.Instance.playerLevel > 1) ShufflePowerup.Instance.SetLocked(false);
            else ShufflePowerup.Instance.SetLocked(true);

            if (PlayerDataManager.Instance.playerLevel > 2) HelperPowerup.Instance.SetLocked(false);
            else HelperPowerup.Instance.SetLocked(true);
        }

        public void LoadLevel(int levelId, bool reset=true)
        {
            if (reset)
                Reset();

            UpdatePowerupButtonStates();

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
            LoadLevel(PlayerDataManager.Instance.playerLevel);
        }

        public void LoadLastLevel(Scene scene, LoadSceneMode mode)
        {
            if (scene.name == "GameScreen" && scene.isLoaded)
                LoadLevel(PlayerDataManager.Instance.playerLevel, reset: false);
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

            Debug.Log($"Loaded {allLevelsData.levelData.Count} levels.");
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

            string filePath = Application.dataPath + "/Resources/Level/Levels.json";

            // Check if the file exists
            if (System.IO.File.Exists(filePath))
            {
                // Delete the existing file
                System.IO.File.Delete(filePath);
            }

            string json = JsonUtility.ToJson(allLevelsData);

            // Write the JSON data to the file
            System.IO.File.WriteAllText(filePath, json);

        }
    }
}
