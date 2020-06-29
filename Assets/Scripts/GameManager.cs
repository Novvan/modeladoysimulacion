using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class GameManager : MonoBehaviour
{
    public GameObject Enemy;
    public GameObject Player;
    private float _wave = 0f;
    private float _spawnRate = 4;
    public List<Transform> _spawnPositions;
    public List<GameObject> Enemies;
    public List<GameObject> Bullets;
    private Random _rnd = new Random();

    private void Start()
    {
        Bullets = new List<GameObject>();
        Enemies = new List<GameObject>();


        foreach (var enemy in Enemies)
        {
            Debug.Log(enemy.name + enemy.transform.position);
        }
    }

    private void Update()
    {
        if (Enemies.Count == 0)
        {
            _spawn();
        }


        if (Player.GetComponent<ObjectInterface>().isAlive && Enemies.Count != 0)
        {
            foreach (var enemy in Enemies)
            {
                if (enemy.GetComponent<ObjectInterface>().isAlive)
                {
                    _collideCircleCircle(Player.gameObject.transform, 0.5f, enemy.gameObject.transform, 0.5f);

                    foreach (var bullet in Bullets)
                    {
                        _collideCircleCircle( bullet.gameObject.transform,0.5f, enemy.gameObject.transform, 0.5f);
                    }    
                }
            }
        }

        // if (!Player.GetComponent<ObjectInterface>().isAlive)
        // {
        //     if (Input.GetKeyDown(KeyCode.R))
        //     {
        //         Player.transform.position = new Vector3(0, 0, 0);
        //         Player.gameObject.SetActive(true);
        //         Player.GetComponent<ObjectInterface>().isAlive = true;
        //     }
        // }
    }

    private bool _collideCircleCircle(Transform obj1, float obj1Diam, Transform obj2, float obj2Diam)
    {
        Vector3 dist = obj1.position - obj2.position;
        bool _flag = dist.magnitude <= obj1Diam + obj2Diam;
        if (_flag)
        {
            //Destroy(obj1.gameObject);
            obj1.gameObject.SetActive(false);
            obj1.GetComponent<ObjectInterface>().isAlive = false;

            //Destroy(obj2.gameObject);
            obj2.gameObject.SetActive(false);
            obj2.GetComponent<ObjectInterface>().isAlive = false;
        }

        return _flag;
    }

    private void _spawn()
    {
        _wave++;
        var _maxAmount = _wave * _spawnRate;
        for (int i = 0; i < _maxAmount; i++)
        {
            Transform _spawnPosition = _spawnPositions[_rnd.Next(0, _spawnPositions.Count)];
            GameObject _newEnemy = Instantiate(Enemy, _spawnPosition.position, _spawnPosition.rotation);
            Debug.Log("Enemy spawned, position: " + _spawnPosition.position);
            Enemies.Add(_newEnemy);
        }
    }
}