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

        void Awake()
        {
            tilePrefab = Resources.Load<GameObject>("Level/Prefabs/Tile");

            tiles = new List<IBaseTile>();
        }

        void Start()
        {
            GameManager.Instance.hitOnTile += SelectTile;
        }

        void Update()
        {
            
        }

        public IBaseTile GetTileById(int id)
        {
            return tiles.Find(x => x.tileId == id);
        }

        public List<IBaseTile> GetTiles()
        {
            return tiles;
        }

        public GameObject SpawnTileAt(Vector3 position)
        {
            var newTile = GameObject.Instantiate(tilePrefab, position, Quaternion.identity);
            tiles.Add(newTile.GetComponent<BaseTile>());

            var tileScript = newTile.GetComponent<BaseTile>();

            newTile.transform.name = tiles.Count.ToString();
            tileScript.SetTileId(tiles.Count);
            tileScript.SetTileType((TileType)(Random.Range(0, 3)));
            tileScript.Lock();

            return newTile;
        }

        public GameObject SpawnTileAt(Vector3 position, TileType tileType)
        {
            var newTile = GameObject.Instantiate(tilePrefab, position, Quaternion.identity);
            tiles.Add(newTile.GetComponent<BaseTile>());

            var tileScript = newTile.GetComponent<BaseTile>();

            newTile.transform.name = tiles.Count.ToString();
            tileScript.SetTileId(tiles.Count);
            tileScript.SetTileType(tileType);

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
            GameObject tileGameObject = hit.collider.gameObject;

            if (!tileGameObject.GetComponent<BaseTile>().isLocked && !tileGameObject.GetComponent<BaseTile>().isInBar)
            {
                TileBarManager.Instance.AddTile(tileGameObject.GetComponent<BaseTile>());
            }
        }
    }
}
