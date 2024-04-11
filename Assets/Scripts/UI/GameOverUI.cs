using System;
using System.Collections.Generic;
using Ludo.Data;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Ludo.UI
{
    [Serializable]
    public class ScoreUI
    {
        public Player Player;
        public TextMeshProUGUI Text;
    }

    public class GameOverUI : MonoBehaviour
    {
        [SerializeField] private List<ScoreUI> scoreUIs;

        private void Start()
        {
            var persistentData = FindObjectOfType<PersistentData>();

            foreach (var ui in scoreUIs)
            {
                var gameData = persistentData.GameDataList.Find( g=> g.Player == ui.Player);
                if (gameData != null)
                {
                    ui.Text.text = $"{ui.Player} : {gameData.Score}";
                }
                else
                {
                    ui.Text.text = $"{ui.Player} : Not playing!";
                }
            }
        }

        public void OnMenu() => SceneManager.LoadScene("Menu");
        public void OnRematch() => SceneManager.LoadScene("Game");
    }
}
