using System;

using UnityEngine;
using UnityEngine.InputSystem;

using MatchTile.Utils;

namespace MatchTile.Manager
{
    public class UIManager : SingletonBase<UIManager>
    {
        public void ActivateLevelCompleteScreen()
        {
            GameObject.Find("Canvas").transform.Find("LevelCompleted").gameObject.SetActive(true);
        }

        public void DeactivateLevelCompleteScreen()
        {
            GameObject.Find("Canvas").transform.Find("LevelCompleted").gameObject.SetActive(false);
        }

        public void ActivateLevelFailedScreen()
        {
            GameObject.Find("Canvas").transform.Find("LevelFailed").gameObject.SetActive(true);
        }

        public void DeactivateLevelFailedScreen()
        {
            GameObject.Find("Canvas").transform.Find("LevelFailed").gameObject.SetActive(false);
        }

        public void ActivateAllLevelsClearedScreen()
        {
            GameObject.Find("Canvas").transform.Find("LevelsFinished").gameObject.SetActive(true);
        }

        public void DeactivateAllLevelsClearedScreen()
        {
            GameObject.Find("Canvas").transform.Find("LevelsFinished").gameObject.SetActive(false);
        }
    }
}
