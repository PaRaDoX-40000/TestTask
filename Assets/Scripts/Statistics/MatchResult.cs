using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchResult : MonoBehaviour
{
    [SerializeField] private int _needVictoryPoints;
    [SerializeField] private int _restartExpectations;
    [SerializeField] private MatchResultUi _matchResultUi;
    [SerializeField] private TablePlayers _tablePlayers;
    [SerializeField] private GameNetwork _gameNetwork;

    private void OnEnable()
    {
        _tablePlayers.PointsChangedEvent += CheckEndMatch;
    }
    private void OnDisable()
    {
        _tablePlayers.PointsChangedEvent -= CheckEndMatch;
    }

    private void CheckEndMatch(string nickname, int points)
    {
        if(points>= _needVictoryPoints)
        {
            PlayerVictory(nickname);
        }
    }

    private void PlayerVictory(string nickname)
    {
        _matchResultUi.ShowResultPanel(nickname, _restartExpectations);
        _matchResultUi.gameObject.SetActive(true);
        StartCoroutine(RestartGame());
    }

    private IEnumerator RestartGame()
    {
        int time = 0;
        while(_restartExpectations > time)
        {
            yield return new WaitForSeconds(1);
            time++;
            _matchResultUi.UpdateCounter(_restartExpectations - time);
        }
        _matchResultUi.gameObject.SetActive(false);
        _gameNetwork.RestartGame();
    }


}
