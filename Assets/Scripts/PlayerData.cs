using System;
using TMPro;
using UnityEngine;

namespace ludo
{
    [Serializable]
    public enum Player
    {
        None,
        RED,
        GREEN,
        BLUE,
        YELLOW
    }
    
    [Serializable]
    public class PlayerData
    {
        public Player Player => player;
        public Color32 Color => color;
        public Tile SourceTile => sourceTile;
        public TextMeshPro ScoreText => scoreText;
        [SerializeField] private Player player;
        [SerializeField] private Color32 color;
        [SerializeField] private Tile sourceTile;
        [SerializeField] private TextMeshPro scoreText;
    }
}