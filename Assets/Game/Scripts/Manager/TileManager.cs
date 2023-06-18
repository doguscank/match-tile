using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MatchTile.Utils;
using MatchTile.Tile;

namespace MatchTile.Manager
{
    public class TileManager : SingletonBase<TileManager>
    {
        [SerializeField]
        private List<IBaseTile> tiles;
        [SerializeField]
        private GameObject tilePrefab;

        void Awake()
        {
            tilePrefab = Resources.Load<GameObject>("Level/Prefabs/Tile");
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

            newTile.GetComponent<BaseTile>().SetTileId(tiles.Count);
            newTile.GetComponent<BaseTile>().SetTileType(TileType.Tile0);
        }

        public bool CheckTileExists(Vector3 position)
        {
            return tiles.Find(x => x.gridPosition == position) != null;
        }

        public void SelectTile(RaycastHit2D hit)
        {
            GameObject tileGameObject = hit.collider.gameObject;

            TileBarManager.Instance.AddTile(tileGameObject.GetComponent<BaseTile>());
        }
    }
}
