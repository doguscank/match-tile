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
        [SerializeField]
        public bool isDebug { get; private set; } = false;
        public bool isEditor { get; private set; } = false;

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
        }

        private void CheckClick()
        {
            Vector3 inputPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(inputPosition, Vector2.zero);
            // Check if clicked on a tile
            if (hit.collider != null)
            {
                hitOnTile?.Invoke(hit);
            }
            else
            {
                hitOnVoid?.Invoke(hit);
            }
        }
    }
}
