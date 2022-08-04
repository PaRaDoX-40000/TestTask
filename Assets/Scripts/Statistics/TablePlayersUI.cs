using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TablePlayersUI : MonoBehaviour
{
    [SerializeField] private Transform _playerPointsPanelsConnector;
    [SerializeField] private PlayerPointsPanel _playerPointsPanelsPrefab;
    [SerializeField] private List <PlayerPointsPanel> _playerPointsPanels;

    public void ShowPlayers(Dictionary<string,int> playerPoints)
    {
        if(_playerPointsPanels.Count < playerPoints.Count)
        {
            AddPlayerPointsPanels(playerPoints.Count - _playerPointsPanels.Count);
        }

        Dictionary<string, int>.KeyCollection playersNickname = playerPoints.Keys;

        int i = 0;
        foreach(string playerNickname in playersNickname)
        {
            _playerPointsPanels[i].SetPlayer(playerNickname, playerPoints[playerNickname]);
            _playerPointsPanels[i].gameObject.SetActive(true);
            i++;
        }
        for(int j=i;j< _playerPointsPanels.Count; j++)
        {
            _playerPointsPanels[j].gameObject.SetActive(false);
        }        
    }

    private void AddPlayerPointsPanels(int quantity)
    {
        for (int i = 0; i < quantity; i++)
        {
            PlayerPointsPanel playerPointsPanel = Instantiate(_playerPointsPanelsPrefab, _playerPointsPanelsConnector);
            _playerPointsPanels.Add(playerPointsPanel);
        }
    }
}
