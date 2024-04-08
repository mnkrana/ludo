using UnityEngine;

namespace ludo
{    
    public class Tile : MonoBehaviour
    {
        public string ID {get; private set;}
        public bool LastTile => isLastTile;
        public MoveTile TileToMove => nextTile;

        [SerializeField] private bool isSafeTile;

        [SerializeField] private bool isLastTile;

        [SerializeField] private MoveTile nextTile;


        private void Awake()
        {
            ID = gameObject.name;            
        }    
    }
}