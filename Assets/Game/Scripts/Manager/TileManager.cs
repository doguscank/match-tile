using System.Collections.Generic;
using System.Linq;

using UnityEngine;

using MatchTile.Utils;
using MatchTile.Tile;
using MatchTile.TileBar;

namespace MatchTile.Manager
{   
    [DefaultExecutionOrder(-15)]
    public class TileManager : SingletonBase<TileManager>
    {
        [SerializeField] private List<BaseTile> tiles;
        [SerializeField] private GameObject tilePrefab;
        public int numTiles => tiles.Count;
        
        public GameObject tileObjectsParent { get; private set; }

        void Awake()
        {
            tilePrefab = Resources.Load<GameObject>("Level/Prefabs/Tile");

            tiles = new List<BaseTile>();
            tileObjectsParent = GameObject.FindGameObjectWithTag("TilesObject");
            
            GameManager.Instance.hitOnTile += SelectTile;
        }

        void Start()
        {
            
        }

        void Update()
        {
            
        }

        public void Reset()
        {
            foreach (var tile in tiles)
            {
                if (tile != null && tile.GetGameobject() != null)
                {
                    tile.SetDestroyed();
                    Destroy(tile.GetGameobject());
                }
            }

            tiles.Clear();
        }

        public BaseTile GetTileById(int id)
        {
            return tiles.Find(x => x.tileId == id);
        }

        public List<BaseTile> GetTilesByType(TileType type)
        {
            return tiles.FindAll(x => x.tileType == type);
        }

        public List<BaseTile> GetTilesByTypeFromGrid(TileType type)
        {
            return tiles.FindAll(x => x.tileType == type && !x.isInBar);
        }

        public List<BaseTile> GetTiles()
        {
            return tiles;
        }

        public bool RemoveTile(BaseTile tile)
        {
            return tiles.Remove(tile);
        }

        public BaseTile GetRandomTile()
        {
            return tiles[Random.Range(0, tiles.Count)];
        }

        public BaseTile GetRandomTileFromGrid()
        {
            var gridTiles = tiles.FindAll(x => x.isInBar == false);
            return gridTiles[Random.Range(0, gridTiles.Count)];
        }

        public GameObject SpawnTileAt(Vector3 position)
        {
            return SpawnTileAt(position, (TileType)(Random.Range(0, 3)));
        }

        public GameObject SpawnTileAt(Vector3 position, TileType tileType)
        {
            var newTile = GameObject.Instantiate(tilePrefab, position, Quaternion.identity);
            tiles.Add(newTile.GetComponent<BaseTile>());

            var tileScript = newTile.GetComponent<BaseTile>();

            newTile.transform.name = tiles.Count.ToString();
            tileScript.SetTileId(tiles.Count);
            tileScript.SetTileType(tileType);

            // newTile.transform.SetParent(tileObjectsParent.transform);

            return newTile;
        }

        public void UpdateTileLocks()
        {
            foreach (var tile in tiles)
            {
                tile.CheckParentsExist();
            }
        }

        public void SelectTile(RaycastHit2D hit)
        {
            SelectTile(hit.collider.gameObject.GetComponent<BaseTile>());
        }

        public void SelectTile(BaseTile tile)
        {
            if (!tile.isLocked && !tile.isInBar)
            {
                TileBarManager.Instance.AddTile(tile);
            }
        }
    }
}
