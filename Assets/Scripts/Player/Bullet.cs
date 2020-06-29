using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float _spd;
    private float _angle;
    private Vector2 _force;
    private Vector2 _dir;
    
    public void Shot(Vector2 _direction, float _speed)
    {
        _angle = transform.rotation.z;
        _dir = _direction;
        _spd = _speed;
    }

    private void Update()
    {
        float _rx = transform.position.x;
        float _ry = transform.position.y;
        
        _rx += _spd * _dir.x * Time.deltaTime;
        _ry += _spd * _dir.y * Time.deltaTime;

        transform.position = new Vector3(_rx, _ry, 0);
    }
}