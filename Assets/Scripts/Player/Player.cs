using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Player : NetworkBehaviour //PlayerNetwork
{
    public string nickname;
    [HideInInspector] public bool canMove = true;
    [HideInInspector] public bool canUseSkills = true;
    [HideInInspector] public bool canMoveCamera = true;
    [SyncVar(hook = nameof(InvulnerableChanged))]
    [HideInInspector] public bool invulnerable = false;
    [SerializeField] private Color _invulnerableColor;
    [SerializeField] private float _timeInvulnerable;
    [SyncVar(hook = nameof(PointsChanged))]
    private int _points = 0;
    [SyncVar(hook = nameof(ColorChanged))]
    private Color _currentColor;
    private Color _defaultColor;
    private MeshRenderer _playermRenderer;

    public UnityAction<string, int> OnpointsChanged;
   
    public int Points => _points;

    private void Start()
    {
        _playermRenderer = GetComponent<MeshRenderer>();
        _defaultColor = _playermRenderer.material.color;              
    }

    public void Restart(Vector3 position)
    {
        _points = 0;
        transform.position = position;
        if( TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            rb.velocity = Vector3.zero;
        }
    }

    private void ColorChanged(Color oldValue, Color newValue)
    {
        _playermRenderer.material.color = newValue;
    }

    private void InvulnerableChanged(bool oldValue, bool newValue)
    {
        invulnerable = newValue;      
    }
    private void PointsChanged(int oldValue, int newValue)
    {
        _points = newValue;        
        OnpointsChanged?.Invoke(nickname, _points);
    }

    [Command]
    public void VoidInvulnerableChang(bool Value)
    {      
        invulnerable = Value;
    }

    [Command]
    public void DoHit(Player player)
    {
        player.TakeHit();
    }

    [Command]
    public void AddPoin()
    {
        _points++;
    }
 
    public void Init( string _nickname)
    {
        nickname = _nickname;
    }

    public void TakeHit()
    {
        StartCoroutine(EnableInvulnerability());
    }
    
    private IEnumerator EnableInvulnerability()
    {
        invulnerable = true;
        _currentColor = _invulnerableColor;
        yield return new WaitForSeconds(_timeInvulnerable);
        invulnerable = false;
        _currentColor = _defaultColor;
    }
}
