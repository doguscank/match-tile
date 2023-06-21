using UnityEngine;

using MatchTile.Utils;

namespace MatchTile.Manager
{
    [DefaultExecutionOrder(5)]
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

        public void ActivateRedoPowerupTutorialScreen()
        {
            GameObject.Find("Canvas").transform.Find("RedoTutorial").gameObject.SetActive(true);
        }

        public void DectivateRedoPowerupTutorialScreen()
        {
            GameObject.Find("Canvas").transform.Find("RedoTutorial").gameObject.SetActive(false);
        }

        public void ActivateShufflePowerupTutorialScreen()
        {
            GameObject.Find("Canvas").transform.Find("ShuffleTutorial").gameObject.SetActive(true);
        }

        public void DeactivateShufflePowerupTutorialScreen()
        {
            GameObject.Find("Canvas").transform.Find("ShuffleTutorial").gameObject.SetActive(false);
        }

        public void ActivateHelperPowerupTutorialScreen()
        {
            GameObject.Find("Canvas").transform.Find("HelperTutorial").gameObject.SetActive(true);
        }

        public void DeactivateHelperPowerupTutorialScreen()
        {
            GameObject.Find("Canvas").transform.Find("HelperTutorial").gameObject.SetActive(false);
        }
    }
}
