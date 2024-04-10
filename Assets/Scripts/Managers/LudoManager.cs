using System.Collections.Generic;
using Ludo.Data;
using Ludo.Events;
using Ludo.ScriptableObjects;
using UnityEngine;

namespace Ludo.Managers
{
    public class LudoManager : MonoBehaviour
    {
        [SerializeField] private Config config;
        [SerializeField] private Player myPlayer;
        [SerializeField] private List<Player> playersPlaying;

        public int DiceNumber {get; private set;}
        public Player MyPlayer => myPlayer;

        private GotiManager _gotiManager;
        private Player _currentPlayer;   
        private int _currentPlayerIndex = 0;     
        private int _multipleSixes = 0;
        private bool _isGameOver;
        
        private void Awake()
        {
            Time.timeScale = config.TimeScale;
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
            if(DiceNumber == 6)
            {
                ++_multipleSixes;
            }
        } 

        public bool IsMyTurn(Player player) => player == _currentPlayer;

        public void ChangeTurn(bool multipleTurn = false)
        {
            if(_isGameOver)
            {
                Debug.Log("Can't change turn anymore - Game is already over!");
                return;
            }

            if(multipleTurn)
            {
                LudoEvents.OnTurnChange?.Invoke(_currentPlayer);
                return;
            }

            if(DiceNumber == 6 && _multipleSixes < 2)
            {
                LudoEvents.OnTurnChange?.Invoke(_currentPlayer);
                return;
            }

            DiceNumber = 0;
            _multipleSixes = 0;

            ++_currentPlayerIndex;            
            if(_currentPlayerIndex == playersPlaying.Count)
            {
                _currentPlayerIndex = 0;
            }
             _currentPlayer = playersPlaying[_currentPlayerIndex];
            LudoEvents.OnTurnChange?.Invoke(_currentPlayer);     
        }

        public void SetGameOver() => _isGameOver = true;
    }
}