using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


public class GameNetwork : NetworkManager
{
    [SerializeField] private Transform[] _spavPoints;
    [SerializeField] private PlayerCollection _playerCollection;     
    private string _nickname;

    private List<string> _nicknames = new List<string>();
   
    public void OnCreateCharacter(NetworkConnectionToClient conn, PlayerStartData message)
    {        
        GameObject playerGameObject = Instantiate(playerPrefab, message.spavPoint, Quaternion.identity);      
        string playerNickname = ChekNickname(message.nickname);
        Player player = playerGameObject.GetComponent<Player>();       
        NetworkServer.AddPlayerForConnection(conn, playerGameObject);
        player.Init(playerNickname);
        _playerCollection.AddPlauer(player);
    }

    private string ChekNickname(string nickname)
    {
        if (_nicknames.Contains(nickname))
        {          
            int i = 2;
            while (_nicknames.Contains(nickname) == true)
            {            
                nickname += i.ToString();
                i++;
            }       
        }
        _nicknames.Add(nickname);
        return nickname;
    }

    public void RestartGame()
    {
        foreach(Player player in _playerCollection.Players)
        {
            player.Restart(_spavPoints[Random.Range(0, _spavPoints.Length)].position);
        }     
    }

    public override void OnStartServer()
    {
        base.OnStartServer();
        NetworkServer.RegisterHandler<PlayerStartData>(OnCreateCharacter);
    }

    public void ActivatePlayerSpawn(string nickname)
    {
        Vector3 spavnPoint = _spavPoints[Random.Range(0, _spavPoints.Length)].position;
        PlayerStartData message = new PlayerStartData() { spavPoint = spavnPoint, nickname = nickname };
        NetworkClient.Send(message);
    }

    public override void OnClientConnect()
    { 
        base.OnClientConnect();  
        ActivatePlayerSpawn(_nickname);
    }
    public void SetNickname(string nickname)
    {
        _nickname = nickname;
    }
    public override void OnServerDisconnect(NetworkConnectionToClient conn)
    {
        _playerCollection.RemovePlayer(conn);
        base.OnServerDisconnect(conn);      
    }

}

public struct PlayerStartData : NetworkMessage 
{
    public Vector3 spavPoint;
    public string nickname;
}
