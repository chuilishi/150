using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private Rigidbody2D _rb;
    public Transform parent;
    [SerializeField]
    private float velocity;

    [SerializeField] private bool direction;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Vector2 vec = parent.position - transform.position;
        _rb.velocity = new Vector2(vec.y,-vec.x).normalized * (velocity * (direction ? 1 : -1));
    }
}
