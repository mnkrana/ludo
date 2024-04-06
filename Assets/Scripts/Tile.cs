using UnityEngine;

namespace ludo
{    
    public class Tile : MonoBehaviour
    {
        public string ID {get; private set;}

        [SerializeField] private bool isSafeTile;

        [SerializeField] private bool isLastTile;

        [SerializeField] private MoveTile nextTile;
    }
}