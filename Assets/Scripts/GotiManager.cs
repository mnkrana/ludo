using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ludo
{
    public class GotiManager : MonoBehaviour
    {
        [SerializeField] private Config config;
        [SerializeField] private GameObject gotiPrefab;
        [SerializeField] private List<PlayerData> players;

        private LudoManager _ludoManager;
        private List<Goti> _goties;
        private List<GameData> _data;

        private void Awake()
        {
            _ludoManager = GetComponent<LudoManager>();
        }

        public void CreateGoties()
        {
            _goties = new List<Goti>();
            _data =  new List<GameData>();

            foreach (var player in players)
            {
                var data = new GameData(player.Player, player.ScoreText);
                _data.Add(data);

                for (var count = config.NumberOfGoti; count > 0; --count)
                {
                    var goti = Instantiate(gotiPrefab).GetComponent<Goti>();
                    goti.SetData(player, data, _ludoManager, this);
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
                return false;
            }
        }
    }
}