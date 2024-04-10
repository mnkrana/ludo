using System.Collections.Generic;
using Ludo.Data;
using Ludo.ScriptableObjects;
using UnityEngine;

namespace Ludo.Core
{
    public class Tile : MonoBehaviour
    {
        [SerializeField] private Config config;
        [SerializeField] private bool isSafeTile;
        [SerializeField] private bool isLastTile;
        [SerializeField] private MoveTile nextTile;

        public bool LastTile => isLastTile;
        public MoveTile TileToMove => nextTile;

        private List<Goti> _goties;

        public void AddGoti(Goti goti)
        {
            if(_goties == null) _goties = new List<Goti>();
            _goties.Add(goti);
            TransformGoties();
        }

        public void RemoveGoti(Goti goti)
        {
            if (_goties.Contains(goti))
            {
                _goties.Remove(goti);
            }
            TransformGoties();
        }

        private void TransformGoties()
        {
            if (_goties.Count == 1)
            {
                _goties[0].transform.position = transform.position;
                _goties[0].ResetScale();
            }
            else
            {
                for (var index = 0; index < _goties.Count; ++index)
                {
                    _goties[index].transform.localScale = 
                        config.GotiScaleOnTile;

                    var gotiPos = _goties[index].transform.position;

                    gotiPos.x = transform.position.x + 
                        ((index % 2 == 0) ? -1 : 1) * 
                            config.GotiOffsetOnTile;

                    if (_goties.Count > 2)
                    {
                        gotiPos.y = transform.position.y + 
                            ((index < 2) ? -1 : 1) * 
                                config.GotiOffsetOnTile;
                    }
                    _goties[index].transform.position = gotiPos;
                }
            }
        }

        public (bool isKilled, Goti gotiKilled) CheckKill(
            Player strikingPlayer)
        {
            if (_goties.Count < 2 || isSafeTile) return (false, null);
            if (_goties.Count == 2)
            {
                foreach(var goti in _goties)
                {
                    if(goti.Player != strikingPlayer)
                    {
                        return (true, goti);
                    }
                }
            }
            return (false, null);
        }

        public bool CheckGameOver()
        {
            if(_goties.Count == config.NumberOfGoti && isLastTile)
            {
                return true;
            }                 
            return false;
        }        
    }
}