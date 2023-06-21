using UnityEngine;

namespace MatchTile.Tile
{
    public class TileMovement : MonoBehaviour
    {
        public void MoveToPosition(Vector3 position)
        {
            transform.position = position;
        }
    }
}
