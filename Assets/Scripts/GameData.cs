using TMPro;

namespace ludo
{
    public class GameData
    {
        private int _score;
        private Player _player;
        private TextMeshPro _text;

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