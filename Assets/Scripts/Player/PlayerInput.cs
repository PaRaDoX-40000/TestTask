using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(Player))]
public class PlayerInput : NetworkBehaviour
{
    [SerializeField] private Skill _dash;
    private Mover _mover;
    private Player _player;
    private int _Vertical = 0;
    private int _Horizontal=0;

    void Start()
    {
        _mover = GetComponent<Mover>();
        _player = GetComponent<Player>();
    }


    void Update()
    {
        if (hasAuthority)
        {

            _Vertical = (int)Input.GetAxis("Vertical");
            _Horizontal = (int)Input.GetAxis("Horizontal");
            if (_player.canMove==true)
            {
                _mover.SetMotionVector(new Vector3(_Horizontal, 0, _Vertical));
            }

            if (_player.canUseSkills==true)
            {
                if (Input.GetKeyDown(KeyCode.Mouse1))
                {
                    _dash.ActivateSkill();
                }
            }           
        }
    }
}
