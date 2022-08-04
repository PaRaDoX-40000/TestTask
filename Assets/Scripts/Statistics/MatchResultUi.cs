using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MatchResultUi : MonoBehaviour
{
    [SerializeField] TMP_Text _nicknameText;
    [SerializeField] TMP_Text _counterText;

    public void ShowResultPanel(string nickname,int waitingTime)
    {
        _nicknameText.text = nickname;
        _counterText.text = waitingTime.ToString();
    }

    public void UpdateCounter(int waitingTime)
    {     
        _counterText.text = waitingTime.ToString();
    }




}
