using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


public class Dash : Skill
{
    [SerializeField] private int _range = 3;
    [SerializeField] private int _speed = 1;
    [SerializeField] private Mover _mover;
    [SerializeField] private Rigidbody _playerhRigidbody;
    [SerializeField] private Player _player;
    private Collider _hitCollider;


    private void Start()
    {
        if(TryGetComponent<Collider>(out _hitCollider) == false)
        {
            Debug.LogError("для способности Dash отсутствует коллайдер");
        }
        else
        {
            _hitCollider.enabled = false;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (_player.hasAuthority)
        {
            if (other.gameObject.TryGetComponent<Player>(out Player player))
            {
                if (player.invulnerable == false)
                {
                    _player.DoHit(player);
                    _player.AddPoin();
                }
            }
        }
        
    }

    public override void ActivateSkill()
    {
        if (_active == false && _charged==true && _mover.Direction!=Vector3.zero && _player.invulnerable==false)
            StartCoroutine(StartDesh());
    }

    private IEnumerator StartDesh()
    {       
        ChangeState(true);
        float time = 0;
        Vector3 direction = _mover.Direction;
        
        while (time<=(float)_range / (float)_speed)
        {
            yield return new WaitForFixedUpdate();
            _playerhRigidbody.MovePosition(transform.position + transform.TransformVector(direction) * _speed * Time.deltaTime);
            time += Time.deltaTime;
        }
        ChangeState(false);
        Recharge();
    }

    private void ChangeState(bool State)
    {
        _hitCollider.enabled = State;
        _player.VoidInvulnerableChang(State);
        _active = State;

        _mover.enabled = !State;
        _player.canMove = !State;
        _player.canUseSkills = !State;
        _player.canMoveCamera = !State;

    }



}
