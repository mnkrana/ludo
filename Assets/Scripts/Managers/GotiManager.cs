using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Ludo.Core;
using Ludo.Data;
using Ludo.Events;
using Ludo.ScriptableObjects;
using UnityEngine;

namespace Ludo.Managers
{
    public class GotiManager : MonoBehaviour
    {
        [SerializeField] private Config config;
        [SerializeField] private GameObject gotiPrefab;
        [SerializeField] private List<PlayerData> players;

        public List<GameData> GameDataList {get; private set;}

        private LudoManager _ludoManager;
        private List<Goti> _goties;

        private void Awake()
        {
            _ludoManager = GetComponent<LudoManager>();
        }

        private void OnEnable() => LudoEvents.OnRoll += CheckMove;
        private void OnDisable() => LudoEvents.OnRoll -= CheckMove;

        public void CreateGoties()
        {
            _goties = new List<Goti>();
            GameDataList = new List<GameData>();

            foreach (var p in _ludoManager.PlayersPlaying)            
            {
                var player = players.Find(x => x.Player == p);
                var data = new GameData(player.Player, player.ScoreText);
                GameDataList.Add(data);
                
                for (var count = _ludoManager.NumberOfGoti; count > 0; --count)
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
            return _goties.Where(g => g.Player == player).ToList();
        }

        public bool CanGotiMove(
            int diceNumber,
            Tile sourceTile,
            Player player)
        {
            if (!sourceTile.TileToMove.IsPlayerZone) return true;

            var nextTile = sourceTile.TileToMove.GetNextTile(player);
            if (nextTile != null)
            {
                --diceNumber;
                if (diceNumber == 0) return true;
                return CanGotiMove(diceNumber, nextTile, player);
            }
            else
            {
                return false;
            }
        }

        private void CheckMove(int diceNumber, Player player)
        {
            var goties = FindGotiesByPlayer(player);
            var canMove = false;
            foreach (var goti in goties)
            {
                canMove = CanGotiMove(diceNumber, goti.CurrentTile, player);
                if (canMove) break;
            }
            StartCoroutine(CheckTurn(canMove, diceNumber, player));
        }

        private IEnumerator CheckTurn(bool canMove, int diceNumber, Player player)
        {
            yield return null;
            if (!canMove || _ludoManager.CancleTurnOn3Sixes)
            {
                yield return new WaitForSeconds(config.DelayToChangeTurn);
                _ludoManager.ChangeTurn();
            }
            else
            {
                yield return new WaitForSeconds(config.DelayToMoveAI);
                LudoEvents.OnMove?.Invoke(diceNumber, player);
            }
        }
    }
}