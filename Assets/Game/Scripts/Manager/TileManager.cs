using System.Collections.Generic;

using UnityEngine;

using MatchTile.Utils;
using MatchTile.Tile;
using MatchTile.TileBar;

namespace MatchTile.Manager
{
    public class TileManager : SingletonBase<TileManager>
    {
        [SerializeField] private List<IBaseTile> tiles;
        [SerializeField] private GameObject tilePrefab;
        public int numTiles => tiles.Count;
        
        public GameObject tileObjectsParent { get; private set; }

        void Awake()
        {
            tiles = new List<IBaseTile>();
            tilePrefab = Resources.Load<GameObject>("Level/Prefabs/Tile");
            tileObjectsParent = GameObject.FindGameObjectWithTag("TilesObject");
        }

        void Start()
        {
            GameManager.Instance.hitOnTile += SelectTile;
        }

        void Update()
        {
            
        }

        public void Reset()
        {
            foreach (var tile in tiles)
            {
                Destroy(tile.GetGameobject());
            }

            tiles = new List<IBaseTile>();
        }

        public IBaseTile GetTileById(int id)
        {
            return tiles.Find(x => x.tileId == id);
        }

        public List<IBaseTile> GetTilesByType(TileType type)
        {
            return tiles.FindAll(x => x.tileType == type);
        }

        public List<IBaseTile> GetTiles()
        {
            return tiles;
        }

        public bool RemoveTile(IBaseTile tile)
        {
            return tiles.Remove(tile);
        }

        public IBaseTile GetRandomTile()
        {
            return tiles[Random.Range(0, tiles.Count)];
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

            newTile.transform.SetParent(tileObjectsParent.transform);

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

        public void SelectTile(IBaseTile tile)
        {
            if (!tile.isLocked && !tile.isInBar)
            {
                TileBarManager.Instance.AddTile(tile);
            }
        }
    }
}
