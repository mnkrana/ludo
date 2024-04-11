using TMPro;

namespace Ludo.Data
{
    public class GameData
    {
        public Player Player => _player;
        public int Score => _score;
        private int _score;
        private readonly Player _player;
        private readonly TextMeshPro _text;

        public GameData(Player player, TextMeshPro text)
        {
            _player = player;
            _text = text;
        }

        public void AddScore(int points)
        {
            _score += points;
            _text.text = $"{_score}";
        }

        public void DecreaseScore(int points)
        {
            _score -= points;
            _text.text = $"{_score}";
        }      
    }
}