using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Mover : MonoBehaviour
{
    [SerializeField] private int _speed;
    private Vector3 _direction;
    private Rigidbody _movableRigidbody;

    public Vector3 Direction => _direction;

    void Start()
    {
        _movableRigidbody = GetComponent<Rigidbody>();
    }
     
    private void FixedUpdate()
    {             
        _movableRigidbody.MovePosition(transform.position + transform.TransformVector(_direction)* _speed * Time.deltaTime);
    }

    public void SetMotionVector(Vector3 motionVector)
    {
        _direction = motionVector.normalized;
    }  
}
