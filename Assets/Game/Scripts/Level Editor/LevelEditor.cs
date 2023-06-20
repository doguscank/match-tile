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
        public Action<RaycastHit2D> hitOnVoid;
        public Action<RaycastHit2D> hitOnTile;

        public int layer { get; private set; } = 0;
        public TileType selectedType { get; private set; }
        public GameObject debugTile { get; private set; }

        private void Awake()
        {
            debugTile = GameObject.Find("DebugTile");

            debugTile.GetComponent<IBaseTile>().SetTileType(TileType.Tile0);
        }

        private void Start()
        {
            InputManager.Instance.onKeyboardKeyPressed += OnKeyboardKeyPressed;
        }

        private void Update()
        {
            
        }

        public void OnKeyboardKeyPressed(KeyCode keyCode)
        {
            if (keyCode >= KeyCode.Alpha0 && keyCode <= KeyCode.Alpha9)
            {
                debugTile.GetComponent<IBaseTile>().SetTileType((TileType)(keyCode - KeyCode.Alpha0));
            }
            else if (keyCode == KeyCode.UpArrow)
            {
                layer ++;
            }
            else if (keyCode == KeyCode.DownArrow)
            {
                layer --;
            }
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

        public void Undo()
        {
            throw new System.NotImplementedException();
        }

        public void Redo()
        {
            throw new System.NotImplementedException();
        }
    }
}
