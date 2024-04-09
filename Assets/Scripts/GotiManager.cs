using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ludo
{
    public class GotiManager : MonoBehaviour
    {
        [SerializeField] private GameObject gotiPrefab;
        [SerializeField] private List<PlayerData> players;

        private LudoManager _ludoManager;
        private List<Goti> _goties;

        private void Awake()
        {
            _ludoManager = GetComponent<LudoManager>();
        }

        public void CreateGoties()
        {
            _goties = new List<Goti>();

            foreach (var player in players)
            {
                for (var count = 4; count > 0; --count)
                {
                    var goti = Instantiate(gotiPrefab).GetComponent<Goti>();
                    goti.SetData(player, _ludoManager, this);
                    if (player.Player != _ludoManager.MyPlayer)
                    {
                        goti.DisableCollider();
                    }
                    _goties.Add(goti);
                }
            }
        }
    
        public List<Goti> FindGotiesByPlayer(Player player)
        {
            return _goties.Where( g => g.Player == player).ToList();
        }

        public bool CanGotiMove(int diceNumber, Tile sourceTile, Player player)
        {
            if(!sourceTile.TileToMove.IsPlayerZone) return true;

            var nextTile = sourceTile.TileToMove.GetNextTile(player);
            if(nextTile != null)
            {
                --diceNumber;
                if(diceNumber == 0) return true;
                return CanGotiMove(diceNumber, nextTile, player);
            }
            else
            {
                Debug.LogError("Can't move!");
                return false;
            }
        }
    }
}