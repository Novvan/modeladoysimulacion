using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject TargetPlayer;
    private Vector3 _direction;
    [SerializeField] private float _speed;

    void Update()
    {
        _direction = TargetPlayer.transform.position - transform.position;
        
        transform.position += _direction.normalized * _speed * Time.deltaTime;
    }
}