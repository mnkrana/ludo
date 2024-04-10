using Ludo.Data;
using Ludo.Events;
using UnityEngine;
using UnityEngine.UI;

namespace Ludo.UI
{
    public class PlayerUI : MonoBehaviour
    {
        [SerializeField] private Image turnStatusImage;
        [SerializeField] private Player player;
        
        public Image DiceNumberImage  => _diceNumberImage;

        private Image _diceNumberImage;
        
        private void OnEnable() => LudoEvents.OnTurnChange += OnTurnChange;

        private void OnDisable() => LudoEvents.OnTurnChange -= OnTurnChange;

        private void OnTurnChange(Player playerTurn)
        {            
            turnStatusImage.enabled = playerTurn == player;     
        }
    }
}