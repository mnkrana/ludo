using System.Collections;
using UnityEngine;

namespace ludo
{
    public class Goti : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;

        private Player _player;
        private Tile _sourceTile;
        private Tile _currentTile;
        private LudoManager _ludoManager;

        public void SetData(PlayerData player, LudoManager ludoManager)
        {
            spriteRenderer.color = player.Color;       
            _sourceTile = player.SourceTile;
            _player = player.Player;

            transform.position = _sourceTile.transform.position;
            _currentTile = _sourceTile;

            _ludoManager = ludoManager;
        }

        private void OnMouseDown()
        {
             if(!_ludoManager.IsMyTurn(_player)) return;
             Debug.Log($"Goti clicked {_player}");
             StartCoroutine(MoveTile());
        }

        private IEnumerator MoveTile()
        {
            for(var diceNumber = _ludoManager.DiceNumber; diceNumber > 0; --diceNumber)
            {            
                var nextTile =  _currentTile.TileToMove.GetNextTile(_player);
                transform.position = nextTile.transform.position;
                _currentTile = nextTile;
                yield return new WaitForSeconds(0.25f);
            }

            _ludoManager.ChangeTurn();
        }
    }
}