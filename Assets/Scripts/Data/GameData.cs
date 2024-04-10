using TMPro;

namespace Ludo.Data
{
    public class GameData
    {
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

        public int GetScore(Player player)
        {   
            if(player == _player)
            {
                return _score;
            }
            return -1;
        }
    }
}