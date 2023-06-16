using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MatchTile.Utils;
using MatchTile.Tile;

namespace MatchTile.Manager
{
    public class TileManager : SingletoneBase<TileManager>
    {
        [SerializeField]
        private List<IBaseTile> tiles;
        private GameObject tilePrefab;

        void Awake()
        {
            tilePrefab = Resources.Load<GameObject>("Game/Level/Prefabs/Tile");
        }

        void Start()
        {
            
        }

        void Update()
        {
            
        }

        public IBaseTile GetTileById(int id)
        {
            return tiles.Find(x => x.tileId == id);
        }

        public void SpawnTileAt(Vector3 position)
        {
            var newTile = GameObject.Instantiate(tilePrefab, position, Quaternion.identity);
            tiles.Add(newTile.GetComponent<BaseTile>());
        }

        public bool CheckTileExists(Vector3 position)
        {
            return tiles.Find(x => x.gridPosition == position) != null;
        }
    }
}
