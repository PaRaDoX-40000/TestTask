using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerPointsPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text _pointsText;
    [SerializeField] private TMP_Text _nickname;

    public void SetPlayer(string nickname, int points)
    {
        _pointsText.text = points.ToString();
        _nickname.text = nickname;
    }
}
