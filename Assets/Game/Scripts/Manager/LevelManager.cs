using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using MatchTile.Level;
using MatchTile.Tile;
using MatchTile.Utils;

namespace MatchTile.Manager
{
    public class LevelManager : SingletonBase<LevelManager>
    {
        public List<LevelDatum> levelData { get; private set; }

        private void Awake()
        {
            levelData = new List<LevelDatum>();
        }

        private void Start()
        {
            
        }

        private void Update()
        {
            
        }

        public void LoadLevel(int levelId)
        {
            if (levelId >= levelData.Count)
                throw new System.IndexOutOfRangeException($"Level with Id {levelId} is not available!");

            var level = levelData[levelId];

            foreach (var tileData in level.tileData)
            {
                TileManager.Instance.SpawnTileAt(tileData.position, tileData.type);
            }
        }

        public void LoadLevels()
        {
            throw new System.NotImplementedException();
        }

        public void SaveLevel()
        {
            if (!GameManager.Instance.isEditor) return;

            List<TileDatum> tileData = new List<TileDatum>();

            foreach (IBaseTile tile in TileManager.Instance.GetTiles())
            {
                tileData.Add(new TileDatum(tile.GetPosition(), tile.tileType));
            }

            LevelDatum level = new LevelDatum(levelData.Count, tileData);
        }
    }
}
