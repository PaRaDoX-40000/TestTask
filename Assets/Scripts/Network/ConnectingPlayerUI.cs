using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ConnectingPlayerUI : MonoBehaviour
{
    [SerializeField] private Button _hostButton;
    [SerializeField] private Button _clientButton;
    [SerializeField] private TMP_InputField _inputIP;
    [SerializeField] private TMP_InputField _inputNickname;
    [SerializeField] private TMP_Text _messageText;

    public TMP_InputField InputIP => _inputIP;
    public TMP_InputField InputNickname => _inputNickname;

    public void Start()
    {
        _inputIP.text = "localhost";
        _messageText.text = "Перед началом игры введите никнейм";
    }

    public void AddMessage(string message)
    {
        _messageText.text += "\n" + message;
    }
}
