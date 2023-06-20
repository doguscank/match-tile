using System;

using UnityEngine;

using MatchTile.Utils;

namespace MatchTile.Manager
{
    public class GameManager : SingletonBase<GameManager>
    {
        public Action<RaycastHit2D> hitOnVoid;
        public Action<RaycastHit2D> hitOnTile;
        public Action<RaycastHit2D> hitOnPowerup;

        private void Awake()
        {

        }

        private void Start()
        {
#if UNITY_EDITOR
            GameObject.Find("Canvas").transform.Find("SaveLevelButton").gameObject.SetActive(true);
#else
            GameObject.Find("DebugTile").SetActive(false);
            InputManager.Instance.onLeftClick += OnTap;
#endif

            // TileManager.Instance.SpawnTileAt(new Vector3(0f, 0f, 0f), Tile.TileType.Tile0);

            // TileManager.Instance.SpawnTileAt(new Vector3(-0.75f, 0.75f, 1f), Tile.TileType.Tile1);
            // TileManager.Instance.SpawnTileAt(new Vector3(0.75f, 0.75f, 1f), Tile.TileType.Tile1);
            // TileManager.Instance.SpawnTileAt(new Vector3(-0.75f, -0.75f, 1f), Tile.TileType.Tile2);
            // TileManager.Instance.SpawnTileAt(new Vector3(0.75f, -0.75f, 1f), Tile.TileType.Tile2);

            // TileManager.Instance.SpawnTileAt(new Vector3(0f, 0f, 2f), Tile.TileType.Tile0);
            // TileManager.Instance.SpawnTileAt(new Vector3(1.5f, -1.5f, 2f), Tile.TileType.Tile2);
            // TileManager.Instance.SpawnTileAt(new Vector3(1.5f, 1.5f, 2f), Tile.TileType.Tile0);
            // TileManager.Instance.SpawnTileAt(new Vector3(-1.5f, -1.5f, 2f), Tile.TileType.Tile1);
            // TileManager.Instance.SpawnTileAt(new Vector3(-1.5f, 1.5f, 2f), Tile.TileType.Tile1);
            // TileManager.Instance.SpawnTileAt(new Vector3(0f, -1.5f, 2f), Tile.TileType.Tile1);
            // TileManager.Instance.SpawnTileAt(new Vector3(0f, 1.5f, 2f), Tile.TileType.Tile1);

        }

        private void Update()
        {

        }

        private void OnTap(Vector2 clickPosition)
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(clickPosition), Vector2.zero);
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
