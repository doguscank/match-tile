using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

using MatchTile.Utils;

namespace MatchTile.Manager
{
    public class GameManager : SingletoneBase<GameManager>
    {
        [SerializeField]
        public bool isDebug { get; private set; } = false;
        public bool isEditor { get; private set; } = false;

        public Action<Vector3> hitOnVoid;
        public Action<Vector3> hitOnTile;

        private void Awake()
        {

        }

        private void Start()
        {
            
        }

        private void Update()
        {
            
        }

        private void CheckClick()
        {
            Vector3 inputPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(inputPosition, Vector2.zero);
            // Check if clicked on a tile
            if (hit.collider != null)
            {
                hitOnTile?.Invoke(inputPosition);
            }
            else
            {
                hitOnVoid?.Invoke(inputPosition);
            }
        }
    }
}
