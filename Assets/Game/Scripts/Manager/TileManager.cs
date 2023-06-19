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

        public GameObject SpawnTileAt(Vector3 position)
        {
            var newTile = GameObject.Instantiate(tilePrefab, position, Quaternion.identity);
            tiles.Add(newTile.GetComponent<BaseTile>());

            var tileScript = newTile.GetComponent<BaseTile>();

            tileScript.SetTileId(tiles.Count);
            tileScript.SetTileType((TileType)(Random.Range(0, 3)));
            tileScript.Lock();

            return newTile;
        }

        public GameObject SpawnTileAt(Vector3 position, TileType tileType, GameObject[] parents)
        {
            var newTile = GameObject.Instantiate(tilePrefab, position, Quaternion.identity);
            tiles.Add(newTile.GetComponent<BaseTile>());

            var tileScript = newTile.GetComponent<BaseTile>();

            tileScript.SetTileId(tiles.Count);
            tileScript.SetTileType(tileType);

            foreach (var parent in parents)
            {
                tileScript.AddParent(parent.GetComponent<BaseTile>());
                parent.GetComponent<BaseTile>().AddChild(tileScript);
            }

            if (parents.Length != 0)
            {
                tileScript.Lock();
            }

            return newTile;
        }

        public bool CheckTileExists(Vector3 position)
        {
            throw new System.NotImplementedException();
        }

        public void SelectTile(RaycastHit2D hit)
        {
            GameObject tileGameObject = hit.collider.gameObject;

            if (!tileGameObject.GetComponent<BaseTile>().isLocked && !tileGameObject.GetComponent<BaseTile>().isInBar)
            {
                TileBarHistory.Instance.AddHistoryNode(tileGameObject.GetComponent<BaseTile>(), tileGameObject.transform.position);
                TileBarManager.Instance.AddTile(tileGameObject.GetComponent<BaseTile>());
            }
        }
    }
}
