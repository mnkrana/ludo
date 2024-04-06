using System;
using UnityEngine;

namespace ludo
{
    [Serializable]
    public class MoveTile
    {
        [SerializeField] private Tile nextTile;

        [SerializeField] private bool isPlayerZone;

        [SerializeField] private Player playerZone;

        [SerializeField] private Tile playerZoneTile;
    }
}