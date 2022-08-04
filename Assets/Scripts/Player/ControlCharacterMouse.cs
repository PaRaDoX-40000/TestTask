using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

[RequireComponent(typeof(Player))]
public class ControlCharacterMouse : NetworkBehaviour
{
    [SerializeField] private Transform _cameraConnector;
    private Player _player;

    private void Start()
    {
        _player = GetComponent<Player>();
        if (_cameraConnector == null)
            _cameraConnector = transform;
    }

    public override void OnStartLocalPlayer()
    {
        Camera.main.transform.SetParent(_cameraConnector);
        Camera.main.transform.localPosition = new Vector3(0, 1.5f, -4.3f);
    }

    void Update()
    {
        if (hasAuthority)
        {
            if (_player.canMoveCamera==true)
            {
                transform.localEulerAngles += new Vector3(0, Input.GetAxis("Mouse X"), 0);
                _cameraConnector.localEulerAngles += new Vector3(-Input.GetAxis("Mouse Y"), 0, 0);

            }         
        }         
    }
}
