using System;
using System.Collections;
using UnityEngine;

namespace ludo
{
    public class Goti : MonoBehaviour
    {
        public Player Player => _player;

        [SerializeField] private Config config;
        [SerializeField] private SpriteRenderer spriteRenderer;

        private Player _player;
        private Tile _sourceTile;
        private Tile _currentTile;
        private LudoManager _ludoManager;
        private GotiManager _gotiManager;
        private GameData _gameData;
        private Vector3 initialScale;

        public void SetData(
            PlayerData player,
            GameData gameData,
            LudoManager ludoManager,
            GotiManager gotiManager)
        {
            spriteRenderer.color = player.Color;
            _sourceTile = player.SourceTile;
            _player = player.Player;

            transform.position = _sourceTile.transform.position;
            _currentTile = _sourceTile;

            _ludoManager = ludoManager;
            _gotiManager = gotiManager;
            _gameData = gameData;

            initialScale = transform.localScale;
            _currentTile.AddGoti(this);
        }

        private void OnMouseDown()
        {
            if (!_ludoManager.IsMyTurn(_player)) return;
            Debug.Log($"Goti clicked {_player}");
            if(!Move())
            {
                Debug.LogError("Please select another Goti!");
            }
        }

        public bool Move()
        {
            if (_gotiManager.CanGotiMove(_ludoManager.DiceNumber, _currentTile, _player))
            {
                StartCoroutine(MoveTile());
                return true;
            }
            else
            {
                return false;
            }
        }

        private IEnumerator MoveTile()
        {
            for (var diceNumber = _ludoManager.DiceNumber; diceNumber > 0; --diceNumber)
            {
                var nextTile = _currentTile.TileToMove.GetNextTile(_player);
                transform.position = nextTile.transform.position;
                _currentTile.RemoveGoti(this);
                _currentTile = nextTile;
                _currentTile.AddGoti(this);
                _gameData.AddScore(config.MovePoints);
                yield return new WaitForSeconds(config.DelayToMove);
            }

            if(_currentTile.CheckGameOver())
            {
                _ludoManager.SetGameOver();
            }

            var strike = _currentTile.CheckKill(_player);
            if (strike.isKilled)
            {
                _gameData.AddScore(config.KillPoints);
                yield return strike.gotiKilled.MoveToSource();
            }

            if(_currentTile.LastTile)
            {
                _gameData.AddScore(config.GoalPoints);
            }

            yield return new WaitForSeconds(config.DelayToChangeTurn);
            _ludoManager.ChangeTurn();
        }

        private IEnumerator MoveToSource()
        {
            while (_currentTile != _sourceTile)
            {
                var prevTile = _currentTile.TileToMove.GetPrevTile();
                transform.position = prevTile.transform.position;
                _currentTile.RemoveGoti(this);
                _currentTile = prevTile;
                _currentTile.AddGoti(this);
                _gameData.DecreaseScore(config.MovePoints);
                yield return new WaitForSeconds(config.DelayToKill);
            }
        }

        public void DisableCollider()
        {
            GetComponent<BoxCollider>().enabled = false;
        }

        public void ResetScale()
        {
            transform.localScale = initialScale;
        }
    }
}