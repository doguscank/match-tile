using UnityEngine;

using TMPro;

using MatchTile.Utils;

namespace MatchTile.Manager
{
    public class MainMenuManager : SingletonBase<MainMenuManager>
    {
        private int resetClicks;

        private void Awake()
        {
            resetClicks = 0;

            if (PlayerDataManager.Instance.playerLevel == 5)
            {
                GameObject.Find("Canvas").transform.Find("ResetLevelsButton").gameObject.SetActive(true);
                GameObject.Find("Canvas").transform.Find("PlayButton").gameObject.SetActive(false);
            }
            else
            {
                GameObject.Find("Canvas").transform.Find("ResetLevelsButton").gameObject.SetActive(false);
                GameObject.Find("Canvas").transform.Find("PlayButton").gameObject.SetActive(true);
            }
        }

        public void Reset()
        {
            if (resetClicks == 0)
            {
                GameObject.Find("Canvas").transform.Find("ResetLevelsButton").Find("ResetLevelsText").GetComponent<TextMeshProUGUI>().text = "CONFIRM";
                resetClicks ++;
            }

            else if(resetClicks == 1)
            {
                PlayerDataManager.Instance.ResetLevels();
                PlayerDataManager.Instance.UpdateUiValues();

                resetClicks = 0;
                GameObject.Find("Canvas").transform.Find("ResetLevelsButton").Find("ResetLevelsText").GetComponent<TextMeshProUGUI>().text = "RESET";

                GameObject.Find("Canvas").transform.Find("ResetLevelsButton").gameObject.SetActive(false);
                GameObject.Find("Canvas").transform.Find("PlayButton").gameObject.SetActive(true);
            }
        }
    }
}