using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.Events;

public class TablePlayers : MonoBehaviour
{
    private Dictionary<string, int> _playerPoints = new Dictionary<string, int>();

    [SerializeField] private GameNetwork _networkManager;
    [SerializeField] private TablePlayersUI _tablePlayersUI;

    public UnityAction<string, int> PointsChangedEvent;



    public void PointsChanged(string playerNickname, int points)
    {
        _playerPoints[playerNickname] = points;
        _tablePlayersUI.ShowPlayers(_playerPoints);
        PointsChangedEvent?.Invoke(playerNickname, points);

    }
    public void AddPlayer(string nickname,int points)
    {      
        _playerPoints.Add(nickname, points);
        _tablePlayersUI.ShowPlayers(_playerPoints);
    }

    public void RemovePlayer(string nickname)
    {
        _playerPoints.Remove(nickname);
        _tablePlayersUI.ShowPlayers(_playerPoints);
    }
}
