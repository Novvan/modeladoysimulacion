using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    [SerializeField] private Transform _targetFollow;

    private void Update()
    {
        transform.position = new Vector3(
            Mathf.Clamp(_targetFollow.position.x,-3.1f,3.1f),
            Mathf.Clamp(_targetFollow.position.y,-2.1f,2.5f),transform.position.z
            );
    }
}
