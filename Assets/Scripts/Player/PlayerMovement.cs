using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float _baseForce = 2.5f;
    private float _baseTorque = 500f;
    private float _friction = 0.999f;
    private float _torque;
    private float _inertia = 1;
    private float _accAng;
    private float _speedAng;
    private Vector2 _force;
    private Vector2 _acc;
    private Vector2 _speed;
    private float _rx;
    private float _ry;
    private float _mass = 1;
    private GameObject _gameObject;

    void Start()
    {
        _gameObject = this.gameObject;
        _force = new Vector2();
        _acc = new Vector2();
        _speed = Vector2.zero;
    }

    void Update()
    {
        _move();
        _gameObject.transform.position = new Vector3(
            Mathf.Clamp(_gameObject.transform.position.x, -11.5f, 11.5f),
            Mathf.Clamp(_gameObject.transform.position.y, -6.5f, 6.9f),
            _gameObject.transform.position.z
        );
    }

    private void _move()
    {
        //angle
        float _angle = this.gameObject.transform.rotation.eulerAngles.z * (float) Math.PI / 180f;

        //nullified variables
        _speed = _speed * _friction;
        _force = Vector2.zero;
        _acc = Vector2.zero;
        _torque = 0;
        _speedAng = 0;
        _accAng = 0;

        //rx & ry
        _rx = this.gameObject.transform.position.x;
        _ry = this.gameObject.transform.position.y;


        //input detection
        if (Input.GetKey(KeyCode.W))
        {
            _force += new Vector2(_baseForce * -(float) Math.Sin(_angle), _baseForce * (float) Math.Cos(_angle));
        }

        if (Input.GetKey(KeyCode.S))
        {
            _force += new Vector2(_baseForce * (float) Math.Sin(_angle), _baseForce * -(float) Math.Cos(_angle));
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            _torque -= Input.GetAxisRaw("Horizontal") * _baseTorque;
        }

        _acc = new Vector2(_force.x / _mass, _force.y / _mass);

        _rx += 0.5f * _acc.x * Time.deltaTime * Time.deltaTime + _speed.x * Time.deltaTime;
        _ry += 0.5f * _acc.y * Time.deltaTime * Time.deltaTime + _speed.y * Time.deltaTime;

        _gameObject.transform.position = new Vector3(_rx, _ry, 0);

        _speed += _acc * Time.deltaTime;

        _accAng = _torque / _inertia;
        _speedAng = _accAng * Time.deltaTime;

        _angle += 0.5f * _accAng * Time.deltaTime * Time.deltaTime + _speedAng * Time.deltaTime;

        _gameObject.transform.eulerAngles = new Vector3(0, 0, _angle * 180 / (float) Math.PI);
    }
}