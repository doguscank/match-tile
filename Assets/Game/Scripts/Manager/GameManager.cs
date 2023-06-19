using System;

using UnityEngine;

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
                InputManager.Instance.onTap += OnTap;
            }

            // float t = 1.5f;

            // for (int i = 0; i < 5; i++)
            // {
            //     for (int j = 0; j < 5; j++)
            //     {
            //         TileManager.Instance.SpawnTileAt(new Vector3(-t * 2 + i * t, -t * 2 + j * t, 0f));
            //     }
            // }

            // for (int i = 0; i < 4; i++)
            // {
            //     for (int j = 0; j < 4; j++)
            //     {
            //         TileManager.Instance.SpawnTileAt(new Vector3(-t * 2 + 0.5f + i * t, -t * 2 + 0.5f + j * t, -1f));
            //     }
            // }

            var layer0_top = TileManager.Instance.SpawnTileAt(new Vector3(0f, 0f, 0f), Tile.TileType.Tile0, new GameObject[] {});
            var layer1_0 = TileManager.Instance.SpawnTileAt(new Vector3(-0.75f, 0.75f, 1f), Tile.TileType.Tile1, new GameObject[] {layer0_top});
            var layer1_1 = TileManager.Instance.SpawnTileAt(new Vector3(0.75f, 0.75f, 1f), Tile.TileType.Tile1, new GameObject[] {layer0_top});
            var layer1_2 = TileManager.Instance.SpawnTileAt(new Vector3(-0.75f, -0.75f, 1f), Tile.TileType.Tile2, new GameObject[] {layer0_top});
            var layer1_3 = TileManager.Instance.SpawnTileAt(new Vector3(0.75f, -0.75f, 1f), Tile.TileType.Tile2, new GameObject[] {layer0_top});
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
