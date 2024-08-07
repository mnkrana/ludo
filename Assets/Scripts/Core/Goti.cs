using System.Collections;
using Ludo.Data;
using Ludo.Managers;
using Ludo.ScriptableObjects;
using UnityEngine;

namespace Ludo.Core
{
    public class Goti : MonoBehaviour
    {
        [SerializeField] private Config config;
        [SerializeField] private SpriteRenderer spriteRenderer;

        public Player Player => _player;
        public Tile CurrentTile => _currentTile;

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
            AudioManager.Instance.Play(AudioName.GOTI_TAP);          
            if(!Move(_ludoManager.DiceNumber))
            {
                Debug.Log("Please select another Goti!");
            }
        }

        public bool Move(int diceNumber)
        {
            if (_gotiManager.CanGotiMove(diceNumber,_currentTile, _player))
            {
                StartCoroutine(MoveGoti(diceNumber));
                return true;
            }
            else
            {                
                return false;
            }
        }

        private IEnumerator MoveGoti(int number)
        {                        
            for (var diceNumber = number; diceNumber > 0; --diceNumber)
            {
                AudioManager.Instance.Play(AudioName.MOVE);
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
                AudioManager.Instance.Play(AudioName.KILL);
                _gameData.AddScore(config.KillPoints);
                yield return new WaitForSeconds(config.DelayBeforeKill);
                yield return strike.gotiKilled.MoveToSource();
            }

            if(_currentTile.LastTile)
            {
                AudioManager.Instance.Play(AudioName.GOAL);
                _gameData.AddScore(config.GoalPoints);
            }

            var multipleTurn = strike.isKilled || _currentTile.LastTile;
            yield return new WaitForSeconds(config.DelayToChangeTurn);
            _ludoManager.ChangeTurn(multipleTurn);
        }

        private IEnumerator MoveToSource()
        {
            while (_currentTile != _sourceTile)
            {
                AudioManager.Instance.Play(AudioName.MOVE);
                var prevTile = _currentTile.TileToMove.GetPrevTile();
                transform.position = prevTile.transform.position;
                _currentTile.RemoveGoti(this);
                _currentTile = prevTile;
                _currentTile.AddGoti(this);
                _gameData.DecreaseScore(config.MovePoints);
                yield return new WaitForSeconds(config.DelayToKill);
            }
        }

        public void DisableCollider() => GetComponent<BoxCollider>().enabled = false;

        public void ResetScale() => transform.localScale = initialScale;
    }
}