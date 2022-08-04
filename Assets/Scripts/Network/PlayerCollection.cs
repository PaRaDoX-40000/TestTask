using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


public class PlayerCollection : NetworkBehaviour
{
    [SerializeField] private TablePlayers _tablePlayers;
    private List<Player> _players = new List<Player>();

    public List<Player> Players => _players;

    public void AddPlauer(Player player)
    {
        _players.Add(player);
        if (isServer)
        {
            UpdatePlayers(_players);
            for(int i =0;i< _players.Count; i++)
            {              
                InitPlauer(_players[i].nickname, i);
            }
        }
    }

    [ClientRpc]
    private void UpdatePlayers(List<Player> players)
    {
        _players = players;
    }

    private void InitPlauerClien(string playerNickname, int index)
    {     
        InitPlauer(playerNickname,index);
    }

    [ClientRpc]
    private void InitPlauer(string playerNickname,int index)
    {
        if (_players[index].nickname == "" || isServer && index == _players.Count - 1)
        {
            _players[index].OnpointsChanged += _tablePlayers.PointsChanged;
            _tablePlayers.AddPlayer(playerNickname, _players[index].Points);
        }       
        _players[index].Init(playerNickname);
    }
    [ClientRpc]
    private void RemovePlayerClient(string nickname)
    {
        _tablePlayers.RemovePlayer(nickname);
    }

    public void RemovePlayer(NetworkConnectionToClient conn)
    {
        
        foreach (Player playr in _players)
        {         
            if(playr.gameObject.GetComponent<NetworkIdentity>().connectionToClient == conn)
            {
                RemovePlayerClient(playr.nickname);
                _players.Remove(playr);
                UpdatePlayers(_players);
                break;
            }            
        }
    }   

}

