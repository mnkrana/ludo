using System.Collections.Generic;
using Ludo.Data;
using UnityEngine;

public class PersistentData : MonoBehaviour
{
    private int _numberOfPlayers;
    public int NumberOfGoti { get; private set; }
    public List<GameData> GameDataList {get; private set;}


    private void Awake() => DontDestroyOnLoad(this);

    public void SetData(int numberOfPlayers, int numberOfGoti)
    {
        _numberOfPlayers = numberOfPlayers;
        NumberOfGoti = numberOfGoti + 1;
    }

    public void SetGameData(List<GameData> gameDatas) => GameDataList = gameDatas;

    public List<Player> GetPlayers()
    {
        var players = new List<Player>();
        switch (_numberOfPlayers)
        {
            case 0:
                players.Add(Player.BLUE);
                players.Add(Player.GREEN);
                break;
            case 1:
                players.Add(Player.BLUE);
                players.Add(Player.RED);
                players.Add(Player.GREEN);
                break;
            case 2:
                players.Add(Player.BLUE);
                players.Add(Player.RED);
                players.Add(Player.GREEN);
                players.Add(Player.YELLOW);
                break;
        }
        return players;
    }
}
