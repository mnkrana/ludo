using System.Collections.Generic;
using UnityEngine;

namespace ludo
{
    public class Tile : MonoBehaviour
    {
        public string ID { get; private set; }
        public bool LastTile => isLastTile;
        public MoveTile TileToMove => nextTile;

        [SerializeField] private bool isSafeTile;

        [SerializeField] private bool isLastTile;

        [SerializeField] private MoveTile nextTile;

        private List<Goti> _goties;


        private void Awake()
        {
            ID = gameObject.name;
            _goties = new List<Goti>();
        }

        public void AddGoti(Goti goti)
        {
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
                    _goties[index].transform.localScale = new Vector3(0.03f, 0.03f, 1);
                    var gotiPos = _goties[index].transform.position;
                    gotiPos.x = transform.position.x + ((index % 2 == 0) ? -1 : 1) * 0.1f;
                    if(_goties.Count > 2)
                    {
                        gotiPos.y = transform.position.y + ((index < 2) ? -1 : 1) * 0.1f;
                    }
                    _goties[index].transform.position = gotiPos;
                }
            }
        }
    }
}