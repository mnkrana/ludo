using UnityEngine;

namespace ludo
{
    [CreateAssetMenu(menuName = "ludo/Config")]
    public class Config : ScriptableObject
    {
        [Header("Tile")]
        public Vector3 gotiScaleOnTile;
        public float gotiOffsetOnTile;

        [Header("Goti")]
        public float DelayToMove;
        public float DelayToKill;
        public float DelayToChangeTurn;

        [Header("Game")]
        public float TimeScale;
    }
}