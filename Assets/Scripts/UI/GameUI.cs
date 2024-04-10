using System.Collections;
using System.Collections.Generic;
using Ludo.Core;
using Ludo.Data;
using Ludo.Events;
using Ludo.Managers;
using Ludo.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace Ludo.UI
{
    public class GameUI : MonoBehaviour
    {
        [SerializeField] private Config config;
        [SerializeField] private Button rollButton;
        [SerializeField] private Text diceNumberText;
        [SerializeField] private Text turnStatusText;
        
        private Player _player;
        private LudoManager _ludoManager;
        private GotiManager _gotiManager;
        private List<Goti> _goties;

        private void Awake()
        {
            _ludoManager = GetComponent<LudoManager>();
            _gotiManager = GetComponent<GotiManager>();

            _player = _ludoManager.MyPlayer;
            rollButton.onClick.AddListener(RollDice);
        }

        private void OnEnable() => LudoEvents.OnTurnChange += OnTurnChange;

        private void OnDisable() => LudoEvents.OnTurnChange -= OnTurnChange;

        private void OnTurnChange(Player playerTurn)
        {            
            turnStatusText.text = $"{playerTurn}";
            diceNumberText.text = "";
            if(playerTurn == _player)
            {
                rollButton.gameObject.SetActive(true);
            }
            else
            {
                rollButton.gameObject.SetActive(false);
            }
        }

        public void RollDice()
        {
            if (!_ludoManager.IsMyTurn(_player)) return;

            var diceNumber = Random.Range(config.MinDiceNumber, config.MaxDiceNumber);
            diceNumberText.text = $"{diceNumber}";
            _ludoManager.SetDice(diceNumber);
            rollButton.gameObject.SetActive(false);
            StartCoroutine(CanAnyGotiMove(diceNumber));
        }

        private IEnumerator CanAnyGotiMove(int diceNumber)
        {
            if (_goties == null || _goties.Count == 0)
            {
                _goties = _gotiManager.FindGotiesByPlayer(_player);
            }

            var canMove = false;
            foreach (var goti in _goties)
            {
                canMove = _gotiManager.CanGotiMove(
                    diceNumber,
                    goti.CurrentTile,
                    _player);
                    
                if (canMove) break;
            }

            if(!canMove)
            {
                yield return new WaitForSeconds(config.DelayToChangeTurn);
                _ludoManager.ChangeTurn();
            }
        }
    }
}