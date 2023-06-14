using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MatchTile.Tile;

namespace MatchTile.Manager
{
    public class TileManager : MonoBehaviour
    {
        [SerializeField]
        private List<IBaseTile> tiles;

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        public IBaseTile GetTileById(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
