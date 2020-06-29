using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Object = System.Object;
using Random = System.Random;

public class GameManager : MonoBehaviour
{
    public GameObject Enemy;
    public GameObject Player;
    private int _wave = 0;
    private float _spawnRate = 4;
    public List<Transform> _spawnPositions;
    public List<GameObject> Enemies;
    public List<GameObject> Bullets;
    private Random _rnd = new Random();
    private int _activatedEnemies;
    private int _counter = 0;
    [SerializeField] private GameObject _canvas;
    [SerializeField] private GameObject _counterUI;
    [SerializeField] private GameObject _endScore;
    [SerializeField] private GameObject _pointer;

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
        _counterUI.GetComponent<TextMeshProUGUI>().text = _counter.ToString();
        _endScore.GetComponent<TextMeshProUGUI>().text = "Your score: " + _counter;

        if (_wave == 0 || _activatedEnemies == 0)
        {
            _spawn();
        }


        if (Input.GetKeyDown(KeyCode.R) && !Player.GetComponent<ObjectInterface>().IsAlive)
        {
            _canvas.transform.Find("BaseUI").gameObject.SetActive(true);
            _canvas.transform.Find("LossUI").gameObject.SetActive(false);
            float _lerp = Mathf.Lerp(0.1f, 1f, 1f);
            Time.timeScale = _lerp;
            Player.transform.position = new Vector3(0, 0, 0);
            Player.gameObject.SetActive(true);
            Player.GetComponent<PlayerMovement>().Speed = Vector2.zero;
            Player.GetComponent<ObjectInterface>().IsAlive = true;

            foreach (var enemy in Enemies)
            {
                enemy.gameObject.GetComponent<ObjectInterface>().IsAlive = true;
                enemy.transform.position = _spawnPositions[_rnd.Next(0, _spawnPositions.Count)].position;
                _activatedEnemies++;
                //Debug.Log("Activated Enemy forloop index: " + enemy);
            }

            _endScore.GetComponent<TextMeshProUGUI>().text = "Your score: " + _counter;
            _counter = 0;
        }


        if (Player.GetComponent<ObjectInterface>().IsAlive && Enemies.Count != 0)
        {
            foreach (var enemy in Enemies)
            {
                if (enemy.GetComponent<ObjectInterface>().IsAlive)
                {
                    bool _playerHit = _collideCircleCircle(Player.gameObject.transform, 0.5f,
                        enemy.gameObject.transform, 0.5f);

                    if (_playerHit)
                    {
                        float _lerp = Mathf.Lerp(1f, 0.1f, 2.5f);
                        Time.timeScale = _lerp;
                        _canvas.transform.Find("BaseUI").gameObject.SetActive(false);
                        _canvas.transform.Find("LossUI").gameObject.SetActive(true);
                        _pointer.SetActive(false);
                        Cursor.visible = true;
                        //Debug.Log("Player dead");
                    }


                    foreach (var bullet in Bullets)
                    {
                        bool _hit = _collideCircleCircle(bullet.gameObject.transform, 0.5f, enemy.gameObject.transform,
                            0.5f);
                        if (_hit)
                        {
                            _counter++;
                            //if (_counter == 10) Debug.Log("Win");
                            Bullets.Remove(bullet);
                            Destroy(bullet);
                        }
                    }
                }
            }
        }
    }

    private bool _collideCircleCircle(Transform obj1, float obj1Diam, Transform obj2, float obj2Diam)
    {
        Vector3 dist = obj1.position - obj2.position;
        bool _flag = dist.magnitude <= obj1Diam + obj2Diam;
        if (_flag)
        {
            //Destroy(obj1.gameObject);
            //obj1.gameObject.SetActive(false);
            obj1.gameObject.GetComponent<ObjectInterface>().IsAlive = false;

            //Destroy(obj2.gameObject);
            obj2.gameObject.GetComponent<ObjectInterface>().IsAlive = false;
            _activatedEnemies--;
        }

        return _flag;
    }

    private void _spawn()
    {
        if (Player.GetComponent<ObjectInterface>().IsAlive)
        {
            _wave++;
            for (int i = 0; i < _spawnRate; i++)
            {
                int _randomSpawn = _rnd.Next(0, _spawnPositions.Count);
                Transform _spawnPosition = _spawnPositions[_randomSpawn];
                GameObject _newEnemy = Instantiate(Enemy, _spawnPosition.position, _spawnPosition.rotation);
                _newEnemy.GetComponent<Enemy>().TargetPlayer = Player.gameObject;
                Enemies.Add(_newEnemy);
                _activatedEnemies++;
            }
        }


        foreach (var enemy in Enemies)
        {
            if (!enemy.gameObject.GetComponent<ObjectInterface>().IsAlive)
            {
                enemy.gameObject.GetComponent<ObjectInterface>().IsAlive = true;
                enemy.transform.position = _spawnPositions[_rnd.Next(0, _spawnPositions.Count)].position;
                _activatedEnemies++;
                //Debug.Log("Activated Enemy forloop index: " + enemy);
            }
        }
    }
}