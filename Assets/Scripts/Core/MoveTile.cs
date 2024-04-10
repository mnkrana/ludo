using System;
using Ludo.Data;
using UnityEngine;

namespace Ludo.Core
{
    [Serializable]
    public class MoveTile
    {
        [SerializeField] private Tile nextTile;
        [SerializeField] private Tile prevTile;
        [SerializeField] private bool isPlayerZone;
        [SerializeField] private Player playerZone;
        [SerializeField] private Tile playerZoneTile;
        
        public bool IsPlayerZone => isPlayerZone;


        public Tile GetNextTile(Player player)
        {            
            if(isPlayerZone && player == playerZone) return playerZoneTile;
            return nextTile;
        }

        public Tile GetPrevTile() => prevTile;
    }
}