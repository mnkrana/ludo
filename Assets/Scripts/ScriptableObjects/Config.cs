using System.Collections.Generic;
using UnityEngine;

namespace Ludo.ScriptableObjects
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

        [Header("Game")]
        public float DelayToChangeTurn;
        public float DelayToRoll;
        public float TimeScale;
        public int NumberOfGoti;
        public int MinDiceNumber;
        public int MaxDiceNumber;

        [Header("Score")]
        public int MovePoints;
        public int KillPoints;
        public int GoalPoints;

        [Header("Sprites")]
        public List<Sprite> DiceNumbers;
        public Sprite DiceDefault;
    }
}