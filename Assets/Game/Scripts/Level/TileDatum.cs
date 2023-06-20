using System;

using UnityEngine;

using MatchTile.Tile;

namespace MatchTile.Level
{
    [System.Serializable]
    public class TileDatum
    {
        public Vector3 position;
        public TileType type;

        public TileDatum(Vector3 _position, TileType _type)
        {
            position = _position;
            type = _type;
        }
    }
}