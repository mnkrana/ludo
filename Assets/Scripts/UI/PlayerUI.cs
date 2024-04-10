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
    public class PlayerUI : MonoBehaviour
    {
        [SerializeField] private Config config;
        [SerializeField] private Button rollButton;
        [SerializeField] private Image turnStatusImage;
        [SerializeField] private Player player;
        
        public Image DiceNumberImage  => _diceNumberImage;

        private Image _diceNumberImage;
        private LudoManager _ludoManager;
        private GotiManager _gotiManager;
        private List<Goti> _goties;
        

        private void Awake()
        {
            _ludoManager = FindObjectOfType<LudoManager>();
            _gotiManager = FindObjectOfType<GotiManager>();
            _diceNumberImage = rollButton.GetComponent<Image>();
            
            rollButton.onClick.AddListener(RollDice);
        }

        private void OnEnable() => LudoEvents.OnTurnChange += OnTurnChange;

        private void OnDisable() => LudoEvents.OnTurnChange -= OnTurnChange;

        private void OnTurnChange(Player playerTurn)
        {            
            if(playerTurn == player)
            {
                turnStatusImage.enabled = true;
                _diceNumberImage.sprite = config.DiceDefault;
                rollButton.gameObject.SetActive(true);
                rollButton.interactable = true;
            }
            else
            {
                turnStatusImage.enabled = false;
                rollButton.gameObject.SetActive(false);
            }
        }

        public void RollDice()
        {
            if (!_ludoManager.IsMyTurn(player)) return;

            var diceNumber = Random.Range(config.MinDiceNumber, config.MaxDiceNumber);            
            _ludoManager.SetDice(diceNumber);
            _diceNumberImage.sprite = config.DiceNumbers[diceNumber - 1];
            rollButton.interactable = false;
            StartCoroutine(CanAnyGotiMove(diceNumber));
        }

         private IEnumerator CanAnyGotiMove(int diceNumber)
        {
            if (_goties == null || _goties.Count == 0)
            {
                _goties = _gotiManager.FindGotiesByPlayer(player);
            }

            var canMove = false;
            foreach (var goti in _goties)
            {
                canMove = _gotiManager.CanGotiMove(
                    diceNumber,
                    goti.CurrentTile,
                    player);
                    
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