using Ludo.Data;
using Ludo.Events;
using Ludo.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace Ludo.Core
{
    public class Dice : MonoBehaviour
    {
        [SerializeField] private Config config;
        [SerializeField] private Player player;
        private Button _rollButton;
        private Image _diceImage;

        private void Awake()
        {
            _rollButton = GetComponent<Button>();
            _diceImage = GetComponent<Image>();

            _rollButton.onClick.AddListener(OnRollButtonClick);
        }    

        private void OnEnable() => LudoEvents.OnTurnChange += OnTurnChange;
        private void OnDisable() => LudoEvents.OnTurnChange -= OnTurnChange;

        private void OnTurnChange(Player playerTurn)
        {
            if(playerTurn == player)
            {                
                _diceImage.sprite = config.DiceDefault;
                _diceImage.enabled = true;
                _rollButton.interactable = true;
            }
            else
            {
                _diceImage.enabled = false;
                _rollButton.interactable = false;
            }
        }

        private void OnRollButtonClick()
        {
            var diceNumber = Random.Range(config.MinDiceNumber, config.MaxDiceNumber);            
            _diceImage.sprite = config.DiceNumbers[diceNumber - 1];
            _rollButton.interactable = false;
            LudoEvents.OnRoll?.Invoke(diceNumber, player);            
        }

        public void RollByAI()
        {
            Invoke(nameof(OnRollButtonClick), config.DelayToRoll);
        }
    }
}