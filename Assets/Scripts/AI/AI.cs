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

            if(_ludoManager.MyPlayer == player)
            {
                enabled = false;
            }
        }

        private void OnEnable() => LudoEvents.OnTurnChange += OnTurnChange;

        private void OnDisable() => LudoEvents.OnTurnChange -= OnTurnChange;

        private void OnTurnChange(Player playerTurn)
        {
            if (playerTurn == player)
            {
                StartCoroutine(RollDice());
            }
        }

        private IEnumerator RollDice()
        {
            yield return new WaitForSeconds(config.DelayToRoll);
            var diceNumber = Random.Range(config.MinDiceNumber,
             config.MaxDiceNumber);
            _ludoManager.SetDice(diceNumber);
            _playerUI.DiceNumberImage.sprite = config.DiceNumbers[diceNumber - 1];

            if (_tries == null) _tries = new List<int>();
            _tries.Clear();

            var hasMoved = MoveGoti();
            if (!hasMoved)
            {
                yield return new WaitForSeconds(config.DelayToChangeTurn);
                _ludoManager.ChangeTurn();
            }
        }

        private bool MoveGoti()
        {
            if (_tries.Count == config.NumberOfGoti) return false;

            if (_goties == null || _goties.Count == 0)
            {
                _goties = _gotiManager.FindGotiesByPlayer(player);
            }

            var random = Random.Range(0, _goties.Count);

            while (_tries.Contains(random))
            {
                random = Random.Range(0, _goties.Count);
            }

            _tries.Add(random);
            var hasMove = _goties[random].Move();
            if (!hasMove)
            {
                return MoveGoti();
            }
            return hasMove;
        }
    }
}
