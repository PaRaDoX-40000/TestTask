using Mirror;
using System;
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
        if (CheckNickname() == true)
        {
            try
            {
                _gameNetwork.SetNickname(_connectingPlayerUI.InputNickname.text);
                NetworkManager.singleton.StartHost();
                _connectingPlayerUI.gameObject.SetActive(false);
            }
            catch
            {
                _connectingPlayerUI.AddMessage("Сервер запущен");
            }
           
        }                 
    }
    public void StartClient()
    {    
        if (CheckNickname() == true)
        {
            _gameNetwork.networkAddress = _connectingPlayerUI.InputIP.text;
            _gameNetwork.SetNickname(_connectingPlayerUI.InputNickname.text);
            NetworkManager.singleton.StartClient();
            StartCoroutine(Connecting());
            
        }      
    }

    private bool CheckNickname()
    {
        string nickname = _connectingPlayerUI.InputNickname.text;
        nickname = Regex.Replace(nickname," ", "");
        if (nickname != "")
        {          
            return true;
        }
        else
        {
            _connectingPlayerUI.AddMessage("Nickname не введён");
            return false;
        }     
    }

    private IEnumerator Connecting()
    {
        _connectingPlayerUI.AddMessage("Подключение...");
        float time = 0;
        while(time < 2)
        {
            yield return new WaitForEndOfFrame();
            if (NetworkClient.isConnected == true)
            {
                _connectingPlayerUI.gameObject.SetActive(false);
            }
            time += Time.deltaTime;
        }       
        if(NetworkClient.isConnected == false)
        {
            _connectingPlayerUI.AddMessage("Сервер не найден");
            NetworkManager.singleton.StopClient();
        }
       
    }





}
