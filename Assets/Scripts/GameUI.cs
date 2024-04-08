using UnityEngine;
using UnityEngine.UI;

namespace ludo
{
    public class GameUI : MonoBehaviour
    {
        [SerializeField] private Player player;
        [SerializeField] private Button rollButton;
        [SerializeField] private Text diceNumberText;

        private LudoManager _ludoManager;

        private void Awake()
        {
            _ludoManager = GetComponent<LudoManager>();
            rollButton.onClick.AddListener(RollDice);
        }

        private void OnEnable()
        {
            LudoEvents.OnTurnChange += OnTurnChange;
        }

        private void OnDisable()
        {
            LudoEvents.OnTurnChange -= OnTurnChange;            
        }

        private void OnTurnChange(Player playerTurn)
        {
            Debug.Log($"Turn Changed to player {playerTurn}");
            diceNumberText.text = "";
            if(playerTurn == player)
            {
                rollButton.gameObject.SetActive(true);
            }
            else
            {
                rollButton.gameObject.SetActive(false);
            }
        }

        public void RollDice()
        {            
            if(!_ludoManager.IsMyTurn(player)) return;
            
            var diceNumber = Random.Range(1,7);            
            diceNumberText.text = $"{diceNumber}";
            _ludoManager.SetDice(diceNumber);
        }
    }
}