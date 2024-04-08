using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ludo
{
    public class AI : MonoBehaviour
    {
        [SerializeField] private Player player;
        [SerializeField] private Text diceNumberText;

        private LudoManager _ludoManager;
        private GotiManager _gotiManager;
        private List<Goti> _goties;

        private void Awake()
        {
            _ludoManager = GetComponent<LudoManager>();
            _gotiManager = GetComponent<GotiManager>();
        }

        private void OnEnable()
        {
            LudoEvents.OnTurnChange += OnTurnChange;
        }

        private void OnDisable()
        {
            LudoEvents.OnTurnChange -= OnTurnChange;            
        }

        private void OnTurnChange(Player playerTurn)
        {            
            diceNumberText.text = "";
            if(playerTurn == player)
            {
                RollDice();
            }
        }

        private void RollDice()
        {                        
            var diceNumber = Random.Range(1,7);
            diceNumberText.text = $"{diceNumber}";
            _ludoManager.SetDice(diceNumber);

            MoveGoti();
        }

        private void MoveGoti()
        {
            if(_goties == null || _goties.Count == 0)
            {
                _goties = _gotiManager.FindGotiesByPlayer(player);
            }

            var randomGoti = Random.Range(0, _goties.Count);
            _goties[randomGoti].Move();
        }
    }
}
