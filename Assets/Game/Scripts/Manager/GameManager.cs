using System;

using UnityEngine;

using MatchTile.Utils;

namespace MatchTile.Manager
{
    public class GameManager : SingletonBase<GameManager>
    {
        public bool editorMode { get; } = false;

        public Action<RaycastHit2D> hitOnVoid;
        public Action<RaycastHit2D> hitOnTile;
        public Action<RaycastHit2D> hitOnPowerup;

        private void Awake()
        {

        }

        private void Start()
        {
            if (editorMode)
            {
                GameObject.Find("Canvas").transform.Find("SaveLevelButton").gameObject.SetActive(true);
            }
            else
            {
                GameObject.Find("DebugTile").SetActive(false);
                InputManager.Instance.onLeftClick += OnTap;
                InputManager.Instance.onTouch += OnTap;
            }
        }

        private void Update()
        {
            
        }

        private void OnTap(Vector2 clickPosition)
        {
            Debug.Log(clickPosition.ToString());
            Debug.Log(Camera.main.ScreenToWorldPoint(clickPosition).ToString());

            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(clickPosition), Vector2.zero);
            // Check if clicked on a tile
            if (hit.collider != null)
            {
                Handheld.Vibrate();
                hitOnTile?.Invoke(hit);
            }
            else
            {
                hitOnVoid?.Invoke(hit);
            }
        }
    }
}
