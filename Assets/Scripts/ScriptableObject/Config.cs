using UnityEngine;

namespace ludo
{
    [CreateAssetMenu(menuName = "ludo/Config")]
    public class Config : ScriptableObject
    {
        [Header("Tile")]
        public Vector3 GotiScaleOnTile;
        public float GotiOffsetOnTile;

        [Header("Goti")]
        public float DelayToMove;
        public float DelayToKill;
        public float DelayToChangeTurn;

        [Header("Game")]
        public float TimeScale;
        public int NumberOfGoti;

        [Header("Score")]
        public int MovePoints;
        public int KillPoints;
        public int GoalPoints;
    }
}