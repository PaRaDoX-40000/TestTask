using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    [SerializeField] protected float _cooldown = 0.5f;
    protected bool _charged = true;
    protected bool _active = false;

    public virtual void Recharge()
    {
        StartCoroutine(RechargeCoroutine());
    }

    private IEnumerator RechargeCoroutine()
    {
        _charged = false;
        yield return new WaitForSeconds(_cooldown);
        _charged = true;
    }
    abstract public void ActivateSkill();   
}
