using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

using MatchTile.Utils;

namespace MatchTile.Manager
{
    public class GameManager : SingletonBase<GameManager>
    {
        [SerializeField] public bool isDebug { get; private set; } = false;
        [SerializeField] public bool isEditor { get; private set; } = false;

        public Action<RaycastHit2D> hitOnVoid;
        public Action<RaycastHit2D> hitOnTile;

        private void Awake()
        {

        }

        private void Start()
        {
            if (!isEditor)
            {
                hitOnTile += TileManager.Instance.SelectTile;
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                TileManager.Instance.SpawnTileAt(new Vector3(0f, 0f, 0f));
            }

            if (Input.GetMouseButtonDown(0))
            {
                CheckClick();
            }
        }

        private void CheckClick()
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            // Check if clicked on a tile
            if (hit.collider != null)
            {
                Debug.Log("Collided with " + hit.collider.transform.name);
                hitOnTile?.Invoke(hit);
            }
            else
            {
                hitOnVoid?.Invoke(hit);
            }
        }
    }
}
