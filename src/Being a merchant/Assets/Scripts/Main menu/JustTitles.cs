using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JustTitles : MonoBehaviour
{
    [SerializeField] private float speed;

    void Update()
    {
        transform.position += Vector3.up * (speed * Time.deltaTime);
    }
}
