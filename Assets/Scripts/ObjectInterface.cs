using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInterface : MonoBehaviour
{
    private bool _isAlive = true;


    public bool IsAlive
    {
        get => _isAlive;
        set
        {
            _isAlive = value;
            this.transform.gameObject.SetActive(value);
        }
    }
}
