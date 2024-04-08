using System.Collections.Generic;
using UnityEngine;

namespace ludo
{
    public class LudoManager : MonoBehaviour
    {
        public int DiceNumber {get; private set;}
        public Player MyPlayer => myPlayer;

        [SerializeField] private Player myPlayer;
        [SerializeField] private List<Player> playersPlaying;

        private GotiManager _gotiManager;
        private Player _currentPlayer;   
        private int _currentPlayerIndex = 0;     
        
        private void Awake()
        {
            _gotiManager = GetComponent<GotiManager>();
        }

        private void Start()
        {
            _gotiManager.CreateGoties();

            _currentPlayer = playersPlaying[_currentPlayerIndex];
            LudoEvents.OnTurnChange?.Invoke(_currentPlayer);            
        }

        public void SetDice(int diceNumber) 
        {
            DiceNumber = diceNumber;
        }

        public bool IsMyTurn(Player player)
        {
            return player == _currentPlayer;
        }

        public void ChangeTurn()
        {
            ++_currentPlayerIndex;            
            if(_currentPlayerIndex == playersPlaying.Count)
            {
                _currentPlayerIndex = 0;
            }
             _currentPlayer = playersPlaying[_currentPlayerIndex];
            LudoEvents.OnTurnChange?.Invoke(_currentPlayer);     
        }
    }
}