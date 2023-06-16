using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MatchTile.Utils;

namespace MatchTile.Manager
{
    public class GameManager : SingletoneBase<GameManager>
    {
        [SerializeField]
        public bool IsDebug { get; private set; } = false;
        public bool IsEditor { get; private set; } = false;

        private void Awake()
        {

        }

        private void Start()
        {
            
        }

        private void Update()
        {
            
        }
    }
}
