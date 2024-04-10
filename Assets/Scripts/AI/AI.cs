using System.Collections;
using System.Collections.Generic;
using Ludo.Core;
using Ludo.Data;
using Ludo.Events;
using Ludo.Managers;
using Ludo.ScriptableObjects;
using Ludo.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Ludo.Auto
{
    public class AI : MonoBehaviour
    {
        [SerializeField] private Config config;
        [SerializeField] private Player player;
        [SerializeField] private Dice dice;

        private LudoManager _ludoManager;
        private GotiManager _gotiManager;
        private PlayerUI _playerUI;
        private List<Goti> _goties;
        private List<int> _tries;

        private void Awake()
        {
            _ludoManager = FindObjectOfType<LudoManager>();
            _gotiManager = FindObjectOfType<GotiManager>();
            _playerUI = GetComponent<PlayerUI>();

            if (_ludoManager.MyPlayer == player)
            {
                enabled = false;
            }
        }

        private void Start()
        {
            if (_tries == null) _tries = new List<int>();
            if (_goties == null || _goties.Count == 0)
            {
                _goties = _gotiManager.FindGotiesByPlayer(player);
            }
        }

        private void OnEnable()
        {
            LudoEvents.OnTurnChange += OnTurnChange;
            LudoEvents.OnMove += OnMove;
        }

        private void OnDisable()
        {
            LudoEvents.OnTurnChange -= OnTurnChange;
            LudoEvents.OnMove -= OnMove;
        }

        private void OnTurnChange(Player playerTurn)
        {
            if (playerTurn != player) return;            
            dice.RollByAI();      
        }

        private void OnMove(int diceNumber, Player playerTurn)
        {
            if (playerTurn != player) return;            
            _tries.Clear();            
            var random = Random.Range(0, _goties.Count);
            var goti = _goties[random];

            while (_tries.Contains(random) || !_gotiManager.CanGotiMove(diceNumber, goti.CurrentTile, player))
            {
                random = Random.Range(0, _goties.Count);
                goti = _goties[random];
            }

            _tries.Add(random);            
            goti.Move(diceNumber);
        }       
    }
}
