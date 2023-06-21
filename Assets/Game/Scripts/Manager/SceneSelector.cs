using System;

using UnityEngine;
using UnityEngine.SceneManagement;

using MatchTile.Utils;

namespace MatchTile.Manager
{
    public class SceneSelector : SingletonBase<SceneSelector>
    {
        public void LoadGameScene()
        {
            SceneManager.LoadScene("Resources/Level/Scenes/GameScreen", LoadSceneMode.Single);
        }

        public void LoadMainScene()
        {
            SceneManager.LoadScene("Resources/Level/Scenes/MainScreen", LoadSceneMode.Single);
        }
    }
}
