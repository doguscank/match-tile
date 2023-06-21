using System;

using UnityEngine;

using CandyCoded.HapticFeedback;

using MatchTile.Utils;

namespace MatchTile.Manager
{
    [DefaultExecutionOrder(-10)]
    public class GameManager : SingletonBase<GameManager>
    {
        public bool editorMode { get; } = false;
        public bool isWaitingResponse { get; private set; } = false;
        public int winnerCoins { get; } = 100;

        public Action<RaycastHit2D> hitOnVoid;
        public Action<RaycastHit2D> hitOnTile;
        public Action<RaycastHit2D> hitOnPowerup;

        private void Awake()
        {
            if (editorMode)
            {
                GameObject.Find("Canvas").transform.Find("SaveLevelButton").gameObject.SetActive(true);
            }
            else
            {
                GameObject.Find("Canvas").transform.Find("SaveLevelButton").gameObject.SetActive(false);
                GameObject.Find("DebugTile").SetActive(false);
            }
        }

        private void Update()
        {
            if (!isWaitingResponse && !editorMode)
            {
                if (TileBarManager.Instance.isFull)
                {
                    LevelFailed();
                    isWaitingResponse = true;
                }

                if (TileManager.Instance.numTiles == 0)
                {
                    LevelPassed();
                    isWaitingResponse = true;
                }
            }
        }

        private void OnEnable()
        {
            if (!editorMode)
            {
                InputManager.Instance.onLeftClick += OnTap;
                InputManager.Instance.onTouch += OnTap;
            }
        }

        private void OnDisable()
        {
            if (!editorMode)
            {
                InputManager.Instance.onLeftClick -= OnTap;
                InputManager.Instance.onTouch -= OnTap;
            }
        }

        public void ResetIsWaitingResponse()
        {
            isWaitingResponse = false;
        }

        private void OnTap(Vector2 clickPosition)
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(clickPosition), Vector2.zero);
            // Check if clicked on a tile
            if (hit.collider != null)
            {
                HapticFeedback.LightFeedback();
                hitOnTile?.Invoke(hit);
            }
            else
            {
                hitOnVoid?.Invoke(hit);
            }
        }

        public void LevelFailed()
        {
            UIManager.Instance.ActivateLevelFailedScreen();
        }

        public void LevelPassed()
        {
            PlayerDataManager.Instance.AddCoins(winnerCoins);
            PlayerDataManager.Instance.AddStar();
            PlayerDataManager.Instance.LevelUp();

            if (PlayerDataManager.Instance.playerLevel >= 5)
                UIManager.Instance.ActivateAllLevelsClearedScreen();
            else
                UIManager.Instance.ActivateLevelCompleteScreen();
        }
    }
}
