using UnityEngine;

namespace MatchTile.Tile
{
    public class TileMovement : MonoBehaviour
    {
        private void Awake()
        {

        }

        private void Start()
        {

        }

        private void Update()
        {

        }

        public void MoveToTileBar()
        {
            throw new System.NotImplementedException();
        }

        public void MoveFromTileBar()
        {
            throw new System.NotImplementedException();
        }

        public void MoveToPosition(Vector3 position)
        {
            transform.position = position;
        }
    }
}
