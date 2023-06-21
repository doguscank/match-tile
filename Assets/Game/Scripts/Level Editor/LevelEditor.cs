using System;
using System.Collections.Generic;

using UnityEngine;

using MatchTile.Manager;
using MatchTile.Tile;
using MatchTile.Utils;

namespace MatchTile.LevelEditor
{
    public class LevelEditor : SingletonBase<LevelEditor>
    {
        public int layer { get; private set; } = 0;
        public TileType selectedType { get; private set; }
        public GameObject debugTile { get; private set; }

        private void Awake()
        {
            if (GameManager.Instance.editorMode)
            {
                debugTile = GameObject.Find("DebugTile");

                debugTile.GetComponent<BaseTile>().SetTileType(TileType.Tile0);

                InputManager.Instance.onIncreaseTileTypePush += IncreaseTileType;
                InputManager.Instance.onDecreaseTileTypePush += DecreaseTileType;
                InputManager.Instance.onIncreaseTileTypePush += UpdateDebugTile;
                InputManager.Instance.onDecreaseTileTypePush += UpdateDebugTile;
                InputManager.Instance.onIncreaseLayerPush += IncreaseLayer;
                InputManager.Instance.onDecreaseLayerPush += DecreaseLayer;
                InputManager.Instance.onLeftClick += OnTap;
                InputManager.Instance.onRightClick += OnRightClick;
            }
            else
                enabled = false;
        }

        private void Start()
        {

        }

        private void Update()
        {

        }

        public void UpdateDebugTile()
        {
            debugTile.GetComponent<BaseTile>().SetTileType(selectedType);
        }

        public void IncreaseLayer()
        {
            layer++;
        }

        public void DecreaseLayer()
        {
            layer--;
        }

        public void IncreaseTileType()
        {
            if (selectedType != TileType.Tile9)
                selectedType++;
        }

        public void DecreaseTileType()
        {
            if (selectedType != TileType.Tile0)
                selectedType--;
        }

        public Vector2 MapPosition(Vector2 position, int layer)
        {
            float x, y;

            if (layer % 2 == 0) // even layer
            {
                x = Mathf.Round(position.x / 1.5f) * 1.5f;
                y = Mathf.Round(position.y / 1.5f) * 1.5f;
            }
            else // odd layer
            {
                x = Mathf.Round(position.x / 0.75f) * 0.75f;
                y = Mathf.Round(position.y / 0.75f) * 0.75f;
            }

            return new Vector2(x, y);
        }

        public bool IsInBounds(Vector2 position)
        {
            if (position.x >= -6 && position.x <= 6 && position.y >= -6 && position.y <= 9)
                return true;

            return false;
        }

        private void OnTap(Vector2 clickPosition)
        {
            var worldPosition = Camera.main.ScreenToWorldPoint(clickPosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero);

            if ((hit.collider != null && hit.collider.transform.position.z == layer))
                return;            

            Vector2 mappedPosition = MapPosition(worldPosition, layer);
            if (IsInBounds(mappedPosition))
                TileManager.Instance.SpawnTileAt(new Vector3(mappedPosition.x, mappedPosition.y, layer), selectedType);
        }

        private void OnRightClick(Vector2 clickPosition)
        {
            var worldPosition = Camera.main.ScreenToWorldPoint(clickPosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero);

            if (hit.collider != null)
            {
                TileManager.Instance.RemoveTile(hit.collider.gameObject.GetComponent<BaseTile>());
                hit.collider.gameObject.GetComponent<BaseTile>().SetDestroyed();
                Destroy(hit.collider.gameObject);
            }
        }
    }
}
