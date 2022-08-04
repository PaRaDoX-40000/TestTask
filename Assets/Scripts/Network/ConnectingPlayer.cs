using Mirror;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class ConnectingPlayer : MonoBehaviour
{
    [SerializeField] private GameNetwork _gameNetwork;
    [SerializeField] private ConnectingPlayerUI _connectingPlayerUI;  

    public void StartHost()
    {
        if (TryPreparation() == true)
        {
            NetworkManager.singleton.StartHost();
        }                 
    }
    public void StartClient()
    {    
        if (TryPreparation() == true)
        {
            _gameNetwork.networkAddress = _connectingPlayerUI.InputIP.text;
            NetworkManager.singleton.StartClient();
        }      
    }

    private bool TryPreparation()
    {
        string nickname = _connectingPlayerUI.InputNickname.text;
        nickname = Regex.Replace(nickname," ", "");
        if (nickname != "")
        {           
            _connectingPlayerUI.gameObject.SetActive(false);
            _gameNetwork.SetNickname(_connectingPlayerUI.InputNickname.text);
            return true;
        }
        else
        {
            _connectingPlayerUI.AddMessage("nickname не введён");
            return false;
        }     
    }
   


}
