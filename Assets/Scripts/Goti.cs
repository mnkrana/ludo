using System;
using System.Collections;
using UnityEngine;

namespace ludo
{
    public class Goti : MonoBehaviour
    {
        public Player Player => _player;

        [SerializeField] private SpriteRenderer spriteRenderer;

        private Player _player;
        private Tile _sourceTile;
        private Tile _currentTile;
        private LudoManager _ludoManager;
        private Vector3 initialScale;

        public void SetData(PlayerData player, LudoManager ludoManager)
        {
            spriteRenderer.color = player.Color;       
            _sourceTile = player.SourceTile;
            _player = player.Player;

            transform.position = _sourceTile.transform.position;
            _currentTile = _sourceTile;

            _ludoManager = ludoManager;
            initialScale = transform.localScale;
            _currentTile.AddGoti(this);
        }

        private void OnMouseDown()
        {
             if(!_ludoManager.IsMyTurn(_player)) return;
             Debug.Log($"Goti clicked {_player}");
             Move();
        }

        public void Move()
        {
             StartCoroutine(MoveTile());
        }

        private IEnumerator MoveTile()
        {
            for(var diceNumber = _ludoManager.DiceNumber; diceNumber > 0; --diceNumber)
            {            
                var nextTile =  _currentTile.TileToMove.GetNextTile(_player);
                transform.position = nextTile.transform.position;
                _currentTile.RemoveGoti(this);
                _currentTile = nextTile;
                _currentTile.AddGoti(this);
                yield return new WaitForSeconds(0.25f);
            }

            var strike = _currentTile.CheckKill(_player);
            if(strike.isKilled)
            {
                yield return strike.gotiKilled.MoveToSource();
            }

            yield return new WaitForSeconds(1.0f);
            _ludoManager.ChangeTurn();
        }

        private IEnumerator MoveToSource()
        {
            while(_currentTile != _sourceTile)
            {            
                var prevTile =  _currentTile.TileToMove.GetPrevTile();
                transform.position = prevTile.transform.position;
                _currentTile.RemoveGoti(this);
                _currentTile = prevTile;
                _currentTile.AddGoti(this);
                yield return new WaitForSeconds(0.125f);
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