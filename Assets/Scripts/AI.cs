using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ludo
{
    public class AI : MonoBehaviour
    {
        [SerializeField] private Config config;
        [SerializeField] private Player player;
        [SerializeField] private Text diceNumberText;

        private LudoManager _ludoManager;
        private GotiManager _gotiManager;
        private List<Goti> _goties;
        private List<int> _tries;

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

            if(_tries == null) _tries = new List<int>();
            _tries.Clear();

            var hasMoved = MoveGoti();
            if(!hasMoved)
            {
                _ludoManager.ChangeTurn();
            }
        }

        private bool MoveGoti()
        {
            if(_tries.Count == config.NumberOfGoti) return false;

            if(_goties == null || _goties.Count == 0)
            {
                _goties = _gotiManager.FindGotiesByPlayer(player);
            }

            var random = Random.Range(0, _goties.Count);

            while(_tries.Contains(random))
            {
                random = Random.Range(0, _goties.Count);
            }

            _tries.Add(random);
            var hasMove = _goties[random].Move();
            if(!hasMove)
            {
                return MoveGoti();
            }
            return hasMove;
        }
    }
}
