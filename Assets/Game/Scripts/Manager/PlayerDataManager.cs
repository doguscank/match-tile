using UnityEngine;
using UnityEngine.SceneManagement;

using TMPro;

using MatchTile.Utils;

namespace MatchTile.Manager
{
    [DefaultExecutionOrder(-3)]
    public class PlayerDataManager : SingletonBase<PlayerDataManager>
    {
        public int coins { get; private set; }
        public int stars { get; private set; }
        public int playerLevel { get; private set; }

        private void Awake()
        {
            LoadData();

            ResetPlayerPrefs();
        }

        public void UpdateUiValues()
        {
            if (SceneManager.GetActiveScene().name == "GameScreen")
            {
                GameObject.Find("Canvas").transform.Find("LevelText").GetComponent<TextMeshProUGUI>().text = "LEVEL " + (playerLevel + 1).ToString("D2");
            }
            else if (SceneManager.GetActiveScene().name == "MainScreen")
            {
                GameObject.Find("Canvas").transform.Find("PlayButton").Find("PlayButtonText").GetComponent<TextMeshProUGUI>().text = "LEVEL " + (playerLevel + 1).ToString("D2");
                GameObject.Find("Canvas").transform.Find("CoinText").GetComponent<TextMeshProUGUI>().text = coins.ToString("D4");
                GameObject.Find("Canvas").transform.Find("StarText").GetComponent<TextMeshProUGUI>().text = stars.ToString("D2");
            }
        }

        private void LoadData()
        {
            coins = PlayerPrefs.GetInt("Coins", 0);
            stars = PlayerPrefs.GetInt("Stars", 0);
            playerLevel = PlayerPrefs.GetInt("PlayerLevel", 0);

            UpdateUiValues();
        }

        public void SaveData()
        {
            PlayerPrefs.SetInt("Coins", coins);
            PlayerPrefs.SetInt("Stars", stars);
            PlayerPrefs.SetInt("PlayerLevel", playerLevel);
            PlayerPrefs.Save();
        }

        public void ResetPlayerPrefs()
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
        }

        public void ResetLevels()
        {
            playerLevel = 0;

            SaveData();
        }

        public void AddCoins(int amount)
        {
            coins += amount;
            SaveData();
        }

        public void AddStar()
        {
            stars++;
            SaveData();
        }

        public void LevelUp()
        {
            playerLevel++;
            SaveData();
        }
    }
}